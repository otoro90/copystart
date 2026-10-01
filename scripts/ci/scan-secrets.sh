#!/usr/bin/env bash
# Scan a working tree for secrets. Findings are redacted and are not written to a report.
set -euo pipefail

if ! command -v gitleaks >/dev/null 2>&1; then
  echo "gitleaks is not on PATH" >&2
  exit 1
fi

root="${1:-}"
if [[ -z "$root" ]]; then
  root=$(git rev-parse --show-toplevel)
fi

expected="8.30.1"
actual=$(gitleaks version | awk '{print $1}')
if [[ "$actual" != "$expected" ]]; then
  echo "gitleaks ${actual} does not match pinned ${expected}" >&2
  exit 1
fi

# Scan "." after cd. An absolute target rewrites fingerprints, so the
# relative entries in .gitleaksignore stop matching. Gitleaks 8.30.1.
cd "$root"
gitleaks dir . --no-banner --redact --no-color --exit-code 1
