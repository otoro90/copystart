"""Reject pull-request workflows that can publish images or reach the cluster."""

from __future__ import annotations

import re
import sys
from pathlib import Path

USES = re.compile(
    r"^\s*-?\s*uses:\s*[A-Za-z0-9_.-]+/[A-Za-z0-9_.-]+@[0-9a-f]{40}(?:\s+#.*)?$"
)
PERMISSION = re.compile(r"^(\s*)([A-Za-z][A-Za-z0-9-]*):\s*(\S+)\s*(?:#.*)?$")
PERMISSION_KEYS = {
    "actions",
    "artifact-metadata",
    "attestations",
    "checks",
    "code-quality",
    "contents",
    "deployments",
    "discussions",
    "id-token",
    "issues",
    "models",
    "packages",
    "pages",
    "pull-requests",
    "repository-projects",
    "security-events",
    "statuses",
    "vulnerability-alerts",
}
FORBIDDEN = (
    "pull_request_target",
    "${{ secrets",
    "kubectl",
    "kubeconfig",
    "docker push",
    "ghcr.io",
    "self-hosted",
    "OPENBAO",
    "environment:",
)


def main() -> int:
    root = Path(sys.argv[1]).resolve() if len(sys.argv) > 1 else Path(__file__).resolve().parents[2]
    workflows = sorted((root / ".github" / "workflows").glob("*.yml"))
    if not workflows:
        print("WORKFLOW_MISSING: .github/workflows has no workflows")
        return 1
    findings: list[str] = []
    for path in workflows:
        findings.extend(check_workflow(path))
    if findings:
        print("\n".join(findings))
        return 1
    print("WORKFLOW_PRIVILEGES: read-only GitHub-hosted checks; image publication is not configured")
    return 0


def check_workflow(path: Path) -> list[str]:
    text = path.read_text(encoding="utf-8")
    findings: list[str] = []
    rel = path.as_posix()
    for token in FORBIDDEN:
        if token in text:
            findings.append(f"WORKFLOW_PRIVILEGE: {rel} contains {token}")
    if "pull_request:" not in text:
        findings.append(f"WORKFLOW_PRIVILEGE: {rel} does not run on pull_request")
    if "ubuntu-latest" not in text:
        findings.append(f"WORKFLOW_PRIVILEGE: {rel} does not use ubuntu-latest")
    if "contents: read" not in text:
        findings.append(f"WORKFLOW_PRIVILEGE: {rel} does not set contents: read")
    for line in text.splitlines():
        if "uses:" in line and not USES.match(line):
            findings.append(f"WORKFLOW_PRIVILEGE: {rel} has an unpinned action: {line.strip()}")
        match = PERMISSION.match(line)
        if match and match.group(2) in PERMISSION_KEYS:
            key, value = match.group(2), match.group(3)
            if key != "contents" or value != "read":
                findings.append(f"WORKFLOW_PRIVILEGE: {rel} grants {key}: {value}")
    return findings


if __name__ == "__main__":
    sys.exit(main())
