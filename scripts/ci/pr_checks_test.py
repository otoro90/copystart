"""Behavior tests for read-only CI gates."""

from __future__ import annotations

import json
import subprocess
import tempfile
import textwrap
import unittest
from pathlib import Path

import assert_workflow_privileges
import check_targets


def _write(path: Path, text: str) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(text, encoding="utf-8")


def _manifest(
    root: Path,
    components: list[dict[str, object]],
    legacy_paths: list[str] | None = None,
) -> Path:
    path = root / "ci" / "deployable-targets.json"
    _write(
        path,
        json.dumps(
            {
                "legacy": {
                    "publishable": False,
                    "paths": legacy_paths or ["Application/CopyStart.sln"],
                },
                "components": components,
            }
        ),
    )
    return path


class TargetChecks(unittest.TestCase):
    def test_missing_declared_target_fails(self) -> None:
        with tempfile.TemporaryDirectory() as raw:
            root = Path(raw)
            _write(root / "Application" / "CopyStart" / "CopyStart.csproj", "<TargetFramework>net6.0</TargetFramework>\n")
            manifest = _manifest(root, [{"id": "api", "paths": ["src/Api/Api.csproj"]}])
            lines = check_targets.check(root, manifest)
            self.assertTrue(any(line.startswith("TARGET_MISSING:") and "src/Api/Api.csproj" in line for line in lines))

    def test_unexpected_project_fails(self) -> None:
        with tempfile.TemporaryDirectory() as raw:
            root = Path(raw)
            _write(root / "src" / "Api" / "Api.csproj", "<TargetFramework>net10.0</TargetFramework>\n")
            manifest = _manifest(root, [])
            lines = check_targets.check(root, manifest)
            self.assertTrue(any("TARGET_UNEXPECTED:" in line and "src/Api/Api.csproj" in line for line in lines))

    def test_legacy_prototype_is_diagnostic_only(self) -> None:
        with tempfile.TemporaryDirectory() as raw:
            root = Path(raw)
            project = "Application/CopyStart/CopyStart.csproj"
            _write(root / project, "<TargetFramework>net6.0</TargetFramework>\n")
            _write(root / "Application" / "CopyStart.sln", "")
            manifest = _manifest(
                root,
                [],
                ["Application/CopyStart.sln", project],
            )
            lines = check_targets.check(root, manifest)
            self.assertTrue(any(line.startswith("LEGACY_DIAGNOSTIC:") for line in lines))
            self.assertFalse(any(line.startswith("TARGET_") for line in lines))


class WorkflowPrivileges(unittest.TestCase):
    def test_package_write_and_tag_pin_fail(self) -> None:
        text = textwrap.dedent(
            """
            on:
              pull_request:
            permissions:
              contents: read
              packages: write
            jobs:
              publish:
                runs-on: ubuntu-latest
                steps:
                  - uses: actions/checkout@v4
            """
        )
        with tempfile.TemporaryDirectory() as raw:
            path = Path(raw) / "bad.yml"
            path.write_text(text, encoding="utf-8")
            findings = assert_workflow_privileges.check_workflow(path)
            self.assertTrue(any("packages: write" in item for item in findings))
            self.assertTrue(any("unpinned action" in item for item in findings))

    def test_read_only_sha_pin_passes(self) -> None:
        sha = "3d3c42e5aac5ba805825da76410c181273ba90b1"
        text = textwrap.dedent(
            f"""
            on:
              pull_request:
            permissions:
              contents: read
            jobs:
              check:
                runs-on: ubuntu-latest
                steps:
                  - uses: actions/checkout@{sha} # v7.0.1
            """
        )
        with tempfile.TemporaryDirectory() as raw:
            path = Path(raw) / "ok.yml"
            path.write_text(text, encoding="utf-8")
            self.assertEqual(assert_workflow_privileges.check_workflow(path), [])


class ProofCommands(unittest.TestCase):
    def test_repository_scan_honors_ignore_for_absolute_root(self) -> None:
        script = Path(__file__).resolve().parent / "scan-secrets.sh"
        root = Path(__file__).resolve().parents[2]
        completed = subprocess.run(
            [str(script), str(root)],
            check=False,
            capture_output=True,
            text=True,
        )
        combined = completed.stdout + completed.stderr
        self.assertEqual(completed.returncode, 0, "working-tree secret scan failed")
        self.assertIn("no leaks found", combined)

    def test_synthetic_secret_fails_without_printing_the_value(self) -> None:
        token = ("AK" + "IA") + ("IOSF" + "ODNN" + "7EXA" + "MPL1")
        with tempfile.TemporaryDirectory() as raw:
            root = Path(raw)
            (root / "leak.env").write_text(f"aws_key={token}\n", encoding="utf-8")
            completed = subprocess.run(
                [
                    "gitleaks",
                    "dir",
                    str(root),
                    "--no-banner",
                    "--redact",
                    "--no-color",
                    "--exit-code",
                    "1",
                ],
                check=False,
                capture_output=True,
                text=True,
            )
            combined = completed.stdout + completed.stderr
            self.assertNotEqual(completed.returncode, 0)
            self.assertNotIn(token, combined)

    def test_invalid_openspec_change_fails_with_its_name(self) -> None:
        script = Path(__file__).resolve().parent / "validate-openspec.sh"
        workspace = Path(__file__).resolve().parents[2]
        with tempfile.TemporaryDirectory(prefix="tmp-ci-", dir=workspace) as raw:
            root = Path(raw)
            _write(root / "openspec" / "config.yaml", "schema: spec-driven\n")
            change = root / "openspec" / "changes" / "bad-change"
            _write(change / ".openspec.yaml", "schema: spec-driven\n")
            _write(change / "proposal.md", "# incomplete\n")
            subprocess.run(["git", "init"], cwd=root, check=True, capture_output=True)
            subprocess.run(["git", "add", "."], cwd=root, check=True, capture_output=True)
            subprocess.run(
                [
                    "git",
                    "-c",
                    "user.email=ci@example.com",
                    "-c",
                    "user.name=ci",
                    "commit",
                    "-m",
                    "invalid spec",
                ],
                cwd=root,
                check=True,
                capture_output=True,
            )
            completed = subprocess.run(
                [str(script)],
                cwd=root,
                check=False,
                capture_output=True,
                text=True,
            )
            combined = completed.stdout + completed.stderr
            self.assertNotEqual(completed.returncode, 0)
            self.assertIn("bad-change", combined)


if __name__ == "__main__":
    unittest.main()
