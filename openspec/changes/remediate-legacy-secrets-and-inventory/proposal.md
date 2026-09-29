## Why

The historical repository contains database credentials in tracked configuration and has no verified inventory of legacy records or files. Credential rotation and a non-mutating inventory must precede modernization or migration planning.

## What Changes

- Revoke and rotate every credential found in tracked files and assess repository history for exposure.
- Replace committed values with documented local-development placeholders and protected secret injection.
- Produce a read-only inventory of legacy schema, row counts, relationships, identities, files, checksums, and unresolved data-quality issues.
- Define preservation, migration, deletion, and rollback evidence without modifying source data.
- Add automated secret scanning and a release gate preventing recurrence.

## Capabilities

### New Capabilities
- `legacy-security-inventory`: Provides credential-remediation evidence, secret-scanning controls, and a reproducible legacy data/file inventory for migration decisions.

### Modified Capabilities

None.

## Impact

Touches tracked configuration, secret-management documentation, CI checks, and read-only inventory tooling. Credential rotation requires an authorized operator and must not expose replacement values. This change precedes every runtime or migration change.