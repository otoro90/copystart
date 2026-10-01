#!/usr/bin/env bash
# Run the local proofs for read-only PR checks. Does not publish an image.
set -euo pipefail

root=$(git rev-parse --show-toplevel)
cd "$root"
python3 -m unittest discover -s scripts/ci -p 'pr_checks_test.py'
python3 scripts/ci/check_targets.py
python3 scripts/ci/assert_workflow_privileges.py
scripts/ci/scan-secrets.sh
echo "PR check proofs passed; no image was published."
