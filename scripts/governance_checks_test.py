"""Behavior tests for repository governance checks."""

from __future__ import annotations

import tempfile
import unittest
from pathlib import Path

import governance_checks


def _write(path: Path, text: str) -> None:
    path.parent.mkdir(parents=True, exist_ok=True)
    path.write_text(text, encoding="utf-8")


CONTEXT = """# CONTEXT

## Legacy Prototype

| Runtime | .NET 6 MVC (`net6.0`), out of support since 2024-11-12 |

## Target Baseline

| Backend | .NET 10 LTS, EF Core 10, Npgsql 10, Wolverine 6 |
| Frontend | Angular 22 |
"""

SYSTEM = """# SYSTEM

## Definition of Done

- Behavior-focused tests written first and passing.
- `openspec validate <change> --strict` passing.
- `CONTEXT.md` or the relevant `docs/` guide updated.
- `python3 scripts/governance_checks.py` passes.
"""


class LinkChecks(unittest.TestCase):
    def test_missing_relative_link_fails(self) -> None:
        with tempfile.TemporaryDirectory() as raw:
            root = Path(raw)
            _write(root / "docs" / "guide.md", "See [missing](missing.md).\n")
            findings = governance_checks.check_doc_links(root)
            self.assertTrue(any("missing.md" in item for item in findings))

    def test_directory_and_external_links(self) -> None:
        with tempfile.TemporaryDirectory() as raw:
            root = Path(raw)
            _write(root / "docs" / "topics" / "note.md", "# Note\n")
            _write(
                root / "docs" / "guide.md",
                "See [topics](topics/) and [web](https://example.com/docs).\n",
            )
            self.assertEqual(governance_checks.check_doc_links(root), [])


class VersionDriftChecks(unittest.TestCase):
    def test_conflicting_target_version_fails(self) -> None:
        with tempfile.TemporaryDirectory() as raw:
            root = Path(raw)
            _write(root / "CONTEXT.md", CONTEXT)
            _write(root / "docs" / "overview.md", "The API targets .NET 8.\n")
            findings = governance_checks.check_version_drift(root)
            self.assertTrue(any(".NET 8" in item for item in findings))

    def test_legacy_and_stale_mentions_are_allowed(self) -> None:
        with tempfile.TemporaryDirectory() as raw:
            root = Path(raw)
            _write(root / "CONTEXT.md", CONTEXT)
            _write(
                root / "docs" / "reuse.md",
                "The prototype uses .NET 6 and is out of support.\n"
                "GovCore docs still say .NET 8 / Angular 16 and are stale.\n"
                "The target remains .NET 10 and Angular 22.\n",
            )
            self.assertEqual(governance_checks.check_version_drift(root), [])


class RequiredValidationChecks(unittest.TestCase):
    def test_change_without_strict_validation_fails(self) -> None:
        with tempfile.TemporaryDirectory() as raw:
            root = Path(raw)
            _write(root / "SYSTEM.md", SYSTEM)
            change = root / "openspec" / "changes" / "example-change"
            _write(change / ".openspec.yaml", "schema: spec-driven\n")
            _write(change / "tasks.md", "- [ ] 1.1 Do the work\n")
            findings = governance_checks.check_required_validation(root)
            self.assertTrue(any("example-change" in item for item in findings))

    def test_change_with_strict_validation_passes(self) -> None:
        with tempfile.TemporaryDirectory() as raw:
            root = Path(raw)
            _write(root / "SYSTEM.md", SYSTEM)
            change = root / "openspec" / "changes" / "example-change"
            _write(change / ".openspec.yaml", "schema: spec-driven\n")
            _write(
                change / "tasks.md",
                "- [ ] 1.1 Run `openspec validate example-change --strict`\n",
            )
            self.assertEqual(governance_checks.check_required_validation(root), [])


class CustomizationChecks(unittest.TestCase):
    def test_instruction_without_apply_to_fails(self) -> None:
        with tempfile.TemporaryDirectory() as raw:
            root = Path(raw)
            _write(root / "AGENTS.md", "# Agents\n")
            _write(
                root / ".github" / "instructions" / "backend.instructions.md",
                "---\nname: Backend\ndescription: Use for backend work.\n---\n",
            )
            findings = governance_checks.check_customizations(root)
            self.assertTrue(any("applyTo" in item for item in findings))


class RepositoryChecks(unittest.TestCase):
    def test_current_repository_passes(self) -> None:
        root = Path(__file__).resolve().parents[1]
        self.assertEqual(governance_checks.collect(root), [])


if __name__ == "__main__":
    unittest.main()
