#!/usr/bin/env bash
# Install the pinned gitleaks release after verifying its GitHub asset digest.
set -euo pipefail

version="8.30.1"
asset="gitleaks_${version}_linux_x64.tar.gz"
# GitHub release asset digest for gitleaks 8.30.1 linux_x64, retrieved 2026-09-30.
sha256="551f6fc83ea457d62a0d98237cbad105af8d557003051f41f3e7ca7b3f2470eb"
prefix="${1:-/usr/local/bin}"

os=$(uname -s)
arch=$(uname -m)
if [[ "$os" != "Linux" || "$arch" != "x86_64" ]]; then
  echo "install-gitleaks.sh supports Linux x86_64 runners; this host is ${os} ${arch}" >&2
  exit 1
fi

tmpdir=$(mktemp -d)
trap 'rm -rf "$tmpdir"' EXIT
url="https://github.com/gitleaks/gitleaks/releases/download/v${version}/${asset}"
curl -fsSL "$url" -o "${tmpdir}/${asset}"
echo "${sha256}  ${tmpdir}/${asset}" | sha256sum -c -
tar -xzf "${tmpdir}/${asset}" -C "$tmpdir" gitleaks
install -m 0755 "${tmpdir}/gitleaks" "${prefix}/gitleaks"
"${prefix}/gitleaks" version
