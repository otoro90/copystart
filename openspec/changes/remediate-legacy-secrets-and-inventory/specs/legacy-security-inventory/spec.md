## ADDED Requirements

### Requirement: Exposed credentials are revoked safely
The project MUST identify tracked credentials, revoke or rotate them through an authorized secret owner, remove live values from the working tree, and retain non-secret evidence of completion.

#### Scenario: Tracked database credential is found
- **WHEN** secret discovery identifies a credential in current files or Git history
- **THEN** the credential is treated as compromised, rotated outside logs and commits, and replaced by a documented secret reference or inert placeholder

### Requirement: Legacy inventory is non-mutating and reproducible
The project SHALL provide a repeatable read-only inventory of schema objects, row counts, relationships, users, roles, files, sizes, checksums, and data-quality exceptions.

#### Scenario: Inventory is rerun against the same snapshot
- **WHEN** the inventory command is executed twice against unchanged source data
- **THEN** it produces equivalent normalized results without changing database rows or files

### Requirement: Future secret exposure is blocked
The repository MUST scan commits and release inputs for secrets and MUST fail validation when a credential-like value violates the approved policy.

#### Scenario: A credential is added to tracked configuration
- **WHEN** CI scans the proposed change
- **THEN** the check fails without printing the complete credential

### Requirement: Migration evidence preserves rollback
The inventory SHALL identify source snapshots, backup locations, checksums, extraction time, tool version, and unresolved anomalies without deleting source material.

#### Scenario: A later migration requires reconciliation
- **WHEN** target counts or file hashes differ from the baseline
- **THEN** operators can compare them with the recorded source snapshot and restore the preserved source independently