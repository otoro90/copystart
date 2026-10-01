"""Fail closed when a deployable target appears or a declared target is missing."""

from __future__ import annotations

import argparse
import json
import sys
from pathlib import Path

LEGACY_PROJECT = "Application/CopyStart/CopyStart.csproj"
SKIP_PARTS = {".git", "node_modules", "bin", "obj", "wwwroot"}
LOCKFILES = {"package-lock.json", "pnpm-lock.yaml", "yarn.lock"}


def main() -> int:
    parser = argparse.ArgumentParser()
    parser.add_argument("--root", type=Path, default=Path(__file__).resolve().parents[2])
    parser.add_argument(
        "--manifest",
        type=Path,
        default=None,
    )
    args = parser.parse_args()
    root = args.root.resolve()
    manifest_path = args.manifest or (root / "ci" / "deployable-targets.json")
    findings = check(root, manifest_path)
    for line in findings:
        print(line)
    return 1 if any(line.startswith("TARGET_") for line in findings) else 0


def check(root: Path, manifest_path: Path) -> list[str]:
    manifest = json.loads(manifest_path.read_text(encoding="utf-8"))
    legacy_paths = set(manifest["legacy"]["paths"])
    if manifest["legacy"].get("publishable", False):
        raise ValueError("legacy prototype must stay publishable: false")

    declared: set[str] = set()
    for component in manifest["components"]:
        for path in component["paths"]:
            declared.add(path)

    lines = [
        "LEGACY_DIAGNOSTIC: Application/CopyStart.sln is the legacy .NET 6 MVC "
        "prototype and is not a deployable SaaS image.",
        "LEGACY_DIAGNOSTIC: LibMan datatables.net@1.12.1 fails with LIB002. "
        "CI does not build or publish this prototype.",
    ]
    project = root / LEGACY_PROJECT
    if project.is_file() and "net6.0" not in project.read_text(encoding="utf-8"):
        lines.append(
            "TARGET_MISSING: legacy project no longer targets net6.0; "
            "declare the replacement before CI can treat it as a SaaS component"
        )

    discovered = discover(root)
    for path in sorted(discovered - legacy_paths - declared):
        lines.append(f"TARGET_UNEXPECTED: {path} is not declared in {manifest_path.name}")
    for path in sorted(declared - discovered):
        if not (root / path).exists():
            lines.append(f"TARGET_MISSING: declared path does not exist: {path}")
    return lines


def discover(root: Path) -> set[str]:
    found: set[str] = set()
    for path in root.rglob("*"):
        if not path.is_file():
            continue
        relative = path.relative_to(root)
        if any(part in SKIP_PARTS for part in relative.parts):
            continue
        name = path.name
        if (
            name.endswith(".csproj")
            or name.endswith(".sln")
            or name in {"angular.json", "Dockerfile", "libman.json"}
            or name in LOCKFILES
            or name.startswith("Dockerfile.")
        ):
            found.add(relative.as_posix())
    return found


if __name__ == "__main__":
    sys.exit(main())
