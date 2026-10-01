#!/usr/bin/env bash
# Validate OpenSpec changes touched by the diff. Prints the change name on failure.
set -euo pipefail

if ! command -v openspec >/dev/null 2>&1; then
  echo "openspec CLI is not on PATH" >&2
  exit 1
fi

root=$(git rev-parse --show-toplevel)
cd "$root"

names_file=$(mktemp)
files_file=$(mktemp)
trap 'rm -f "$names_file" "$files_file"' EXIT

if [[ -n "${CHANGE_NAMES:-}" ]]; then
  printf '%s\n' $CHANGE_NAMES | awk 'NF' | sort -u > "$names_file"
else
  zero="0000000000000000000000000000000000000000"
  if [[ -n "${BASE_SHA:-}" && "${BASE_SHA}" != "$zero" ]]; then
    if ! git cat-file -e "${BASE_SHA}^{commit}" 2>/dev/null; then
      echo "BASE_SHA is not a commit: ${BASE_SHA}" >&2
      exit 1
    fi
    git diff --name-only "${BASE_SHA}" HEAD -- openspec/changes > "$files_file"
  else
    git diff-tree --root --no-commit-id --name-only -r HEAD -- openspec/changes > "$files_file"
  fi

  while IFS= read -r file; do
    case "$file" in
      openspec/changes/archive/*) continue ;;
      openspec/changes/*/*)
        name=$(printf '%s' "$file" | cut -d/ -f3)
        if [[ -n "$name" && "$name" != "archive" ]]; then
          printf '%s\n' "$name"
        fi
        ;;
    esac
  done < "$files_file" | sort -u > "$names_file"
fi

if [[ ! -s "$names_file" ]]; then
  echo "No OpenSpec change files in the diff; validation skipped."
  exit 0
fi

failed=0
while IFS= read -r name; do
  if [[ ! -f "openspec/changes/${name}/.openspec.yaml" ]]; then
    echo "OpenSpec change directory is missing: ${name}" >&2
    failed=1
    continue
  fi
  echo "Validating OpenSpec change: ${name}"
  if ! openspec validate "$name" --strict --no-interactive; then
    echo "OpenSpec validation failed: ${name}" >&2
    failed=1
  fi
done < "$names_file"

exit "$failed"
