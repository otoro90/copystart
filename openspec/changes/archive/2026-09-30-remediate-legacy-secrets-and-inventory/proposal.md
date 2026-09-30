## Why

The historical development configuration embeds one PostgreSQL credential in `appsettings*.json` and Docker Compose, while the prototype also tracks one PNG under its upload directory. We have not verified that the database credential is still active or found a database dump, so remediation must distinguish exposed legacy configuration from real sources of customer data to preserve.

## What Changes

- Remove working credential values from tracked prototype configuration and replace them with inert examples plus an explicitly local secret mechanism.
- Determine with the database owner whether the historical credential is still accepted by any active service; rotate it only if active or reused, and record a retired status if no such service exists.
- Scan Git history for exposure and record redacted file/commit fingerprints. Do not rewrite history automatically; treat a history purge as a separate coordinated decision after revocation is assessed.
- Produce a read-only, privacy-preserving inventory of the tracked PNG and any legacy database/file source the owner explicitly identifies for preservation.
- If no source database or legacy files are available, record migration inventory as not applicable instead of building a general importer.

## Capabilities

### New Capabilities
- `legacy-security-inventory`: Defines evidence-based legacy credential remediation, scoped read-only inventory, and migration applicability decisions.

### Modified Capabilities

None.

## Impact

Touches only the legacy CopyStart configuration and a read-only inventory manifest/report. It does not rotate mini-cluster OpenBao credentials, change SeaweedFS, or create CI workflows; those belong to the mini-cluster security changes and `establish-github-ci-and-image-publishing`. Any database/file import remains gated on locating an authoritative source.