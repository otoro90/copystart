#!/usr/bin/env bash
# Verify .NET 10 backend foundation: restore, format, build, test, and vulnerability scan.
set -euo pipefail

root=$(git rev-parse --show-toplevel)
cd "$root"

echo "==> Restoring CopyStart.sln..."
dotnet restore CopyStart.sln

echo "==> Verifying formatting..."
dotnet format --verify-no-changes CopyStart.sln

echo "==> Building CopyStart.sln..."
dotnet build CopyStart.sln --no-restore

echo "==> Running automated test suite..."
dotnet test CopyStart.sln --no-build

echo "==> Auditing dependencies for vulnerabilities..."
vuln_output=$(dotnet list CopyStart.sln package --vulnerable --include-transitive)
echo "$vuln_output"

if echo "$vuln_output" | grep -Eiq "(has the following vulnerable packages|tiene los siguientes paquetes vulnerables|\b(Critical|High|Moderate|Low)\b)"; then
  echo "ERROR: Vulnerable packages detected in the target dependency graph." >&2
  exit 1
fi

echo "Backend check passed: restore, format, build, test, and vulnerability audit clean."
