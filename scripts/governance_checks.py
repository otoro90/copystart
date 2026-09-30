"""Check documentation links, target-version drift, and required validation."""

from __future__ import annotations

import re
import sys
import urllib.parse
from pathlib import Path

VERSION_PATTERNS: dict[str, str] = {
    "dotnet": r"\.NET(?:\s+Core)?\s+(\d+)",
    "efcore": r"EF Core\s+(\d+)",
    "npgsql": r"Npgsql\s+(\d+)",
    "wolverine": r"Wolverine(?:Fx)?\s+(\d+)",
    "angular": r"Angular\s+(\d+)",
}

EXEMPT_CLAIM = re.compile(
    r"(?i)(legacy|prototype|out of support|unsupported|stale|historical|net6\.0)"
)
INLINE_LINK = re.compile(r"(?<!!)\[[^\[\]]+\]\(([^)]+)\)|!\[[^\[\]]*\]\(([^)]+)\)")
FENCE = re.compile(r"```.*?```", re.DOTALL)
TARGET_HEADING = "## Target Baseline"


def collect(root: Path) -> list[str]:
    findings: list[str] = []
    findings.extend(check_doc_links(root))
    findings.extend(check_version_drift(root))
    findings.extend(check_required_validation(root))
    findings.extend(check_customizations(root))
    return findings


def check_doc_links(root: Path) -> list[str]:
    findings: list[str] = []
    for path in _authority_markdown(root):
        visible = FENCE.sub("", path.read_text(encoding="utf-8"))
        for match in INLINE_LINK.finditer(visible):
            raw = match.group(1) or match.group(2)
            target = _link_target(raw)
            if target is None:
                continue
            destination = _resolve_link(root, path, target)
            if destination is None:
                findings.append(f"{_rel(root, path)}: link escapes repository: {target}")
                continue
            if not destination.exists():
                findings.append(f"{_rel(root, path)}: broken link {target}")
    return findings


def check_version_drift(root: Path) -> list[str]:
    context_path = root / "CONTEXT.md"
    if not context_path.is_file():
        return ["CONTEXT.md is missing; it is the target-version authority"]
    context = context_path.read_text(encoding="utf-8")
    try:
        canonical = canonical_target_versions(context)
    except ValueError as error:
        return [str(error)]

    findings: list[str] = []
    for path in _version_sources(root):
        text = FENCE.sub("", path.read_text(encoding="utf-8"))
        if path.name == "CONTEXT.md":
            marker = text.find(TARGET_HEADING)
            text = text[marker:] if marker >= 0 else text
        for label, pattern in VERSION_PATTERNS.items():
            for match in re.finditer(pattern, text):
                claimed = int(match.group(1))
                if claimed == canonical[label]:
                    continue
                window = text[max(0, match.start() - 80) : match.end() + 80]
                if EXEMPT_CLAIM.search(window):
                    continue
                findings.append(
                    f"{_rel(root, path)}: {match.group(0)} disagrees with "
                    f"CONTEXT.md Target Baseline {label} {canonical[label]}"
                )
    return findings


def canonical_target_versions(context: str) -> dict[str, int]:
    marker = context.find(TARGET_HEADING)
    if marker < 0:
        raise ValueError("CONTEXT.md is missing ## Target Baseline")
    section = context[marker:]
    next_heading = re.search(r"\n## ", section[len(TARGET_HEADING) :])
    if next_heading:
        section = section[: len(TARGET_HEADING) + next_heading.start()]
    found: dict[str, int] = {}
    for label, pattern in VERSION_PATTERNS.items():
        match = re.search(pattern, section)
        if match is None:
            raise ValueError(f"CONTEXT.md Target Baseline is missing {label}")
        found[label] = int(match.group(1))
    return found


def check_required_validation(root: Path) -> list[str]:
    findings: list[str] = []
    system_path = root / "SYSTEM.md"
    if not system_path.is_file():
        findings.append("SYSTEM.md is missing")
    else:
        system = system_path.read_text(encoding="utf-8")
        for needle in (
            "Behavior-focused tests",
            "openspec validate <change> --strict",
            "CONTEXT.md",
            "python3 scripts/governance_checks.py",
        ):
            if needle not in system:
                findings.append(f"SYSTEM.md Definition of Done is missing {needle!r}")

    changes = root / "openspec" / "changes"
    if not changes.is_dir():
        return findings
    for change in sorted(path for path in changes.iterdir() if path.is_dir()):
        if not (change / ".openspec.yaml").is_file():
            continue
        tasks = change / "tasks.md"
        if not tasks.is_file():
            findings.append(f"{change.name}: tasks.md is missing")
            continue
        text = tasks.read_text(encoding="utf-8")
        command = f"openspec validate {change.name} --strict"
        if command not in text and "strict OpenSpec validation" not in text:
            findings.append(
                f"{change.name}: tasks.md does not require strict OpenSpec validation"
            )
    return findings


def check_customizations(root: Path) -> list[str]:
    findings: list[str] = []
    for name in ("AGENTS.md", "CONTEXT.md", "SYSTEM.md"):
        if not (root / name).is_file():
            findings.append(f"{name} is missing")

    instructions = root / ".github" / "instructions"
    files = sorted(instructions.glob("*.instructions.md")) if instructions.is_dir() else []
    if not files:
        findings.append(".github/instructions has no instruction files")
    for path in files:
        front = _front_matter(path)
        if front is None:
            findings.append(f"{_rel(root, path)}: missing YAML front matter")
            continue
        for key in ("name:", "description:", "applyTo:"):
            if key not in front:
                findings.append(f"{_rel(root, path)}: front matter is missing {key[:-1]}")

    agents = root / ".github" / "agents"
    agent_files = sorted(agents.glob("*.agent.md")) if agents.is_dir() else []
    if not agent_files:
        findings.append(".github/agents has no custom agents")
    for path in agent_files:
        front = _front_matter(path)
        if front is None:
            findings.append(f"{_rel(root, path)}: missing YAML front matter")
            continue
        for key in ("name:", "description:"):
            if key not in front:
                findings.append(f"{_rel(root, path)}: front matter is missing {key[:-1]}")
    return findings


def _authority_markdown(root: Path) -> list[Path]:
    files: list[Path] = []
    for name in ("AGENTS.md", "CONTEXT.md", "SYSTEM.md"):
        path = root / name
        if path.is_file():
            files.append(path)
    docs = root / "docs"
    if docs.is_dir():
        files.extend(sorted(docs.rglob("*.md")))
    github = root / ".github"
    if github.is_dir():
        files.extend(sorted(github.rglob("*.md")))
    return files


def _version_sources(root: Path) -> list[Path]:
    files = _authority_markdown(root)
    for relative in ("openspec/config.yaml", "llms.txt"):
        path = root / relative
        if path.is_file():
            files.append(path)
    return files


def _link_target(raw: str) -> str | None:
    target = raw.strip()
    if target.startswith("<") and ">" in target:
        target = target[1 : target.index(">")]
    else:
        target = target.split()[0]
    if not target or target.startswith(("#", "http://", "https://", "mailto:", "//")):
        return None
    target = target.split("#", 1)[0].split("?", 1)[0]
    target = urllib.parse.unquote(target)
    return target or None


def _resolve_link(root: Path, source: Path, target: str) -> Path | None:
    if target.startswith("/"):
        destination = (root / target.lstrip("/")).resolve()
    else:
        destination = (source.parent / target).resolve()
    try:
        destination.relative_to(root.resolve())
    except ValueError:
        return None
    return destination


def _front_matter(path: Path) -> str | None:
    text = path.read_text(encoding="utf-8")
    if not text.startswith("---\n"):
        return None
    end = text.find("\n---", 4)
    if end < 0:
        return None
    return text[4:end]


def _rel(root: Path, path: Path) -> str:
    return path.resolve().relative_to(root.resolve()).as_posix()


def main() -> int:
    root = Path(__file__).resolve().parents[1]
    findings = collect(root)
    if findings:
        print("\n".join(findings), file=sys.stderr)
        print(f"{len(findings)} governance check(s) failed", file=sys.stderr)
        return 1
    print("Governance checks passed")
    return 0


if __name__ == "__main__":
    sys.exit(main())
