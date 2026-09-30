## ADDED Requirements

### Requirement: Legacy development configuration contains no live credentials
The repository MUST replace live credential values in prototype configuration with inert examples or local-only secret references. An authorized owner SHALL determine whether an exposed value is still active or reused before it is rotated.

#### Scenario: Historical credential is still active
- **WHEN** the authorized service owner confirms a tracked historical credential remains accepted or was reused
- **THEN** the owner rotates the affected credential out of band, verifies rejection of the old value, and records only redacted evidence

### Requirement: Legacy inventory is explicitly scoped and non-mutating
Any database or filesystem inventory SHALL use only an owner-identified, immutable source snapshot with read-only access and SHALL record structural metadata and checksums without row content or personal data.

#### Scenario: No authoritative snapshot is available
- **WHEN** no database or external file snapshot is identified and authorized
- **THEN** the change records that migration inventory as unavailable or not applicable and does not discover unrelated cluster resources

#### Scenario: Inventory is rerun against an unchanged snapshot
- **WHEN** an authorized inventory is executed twice against the same immutable source
- **THEN** it produces equivalent normalized results and does not modify the source

### Requirement: Tracked prototype uploads have non-sensitive integrity evidence
The one tracked prototype PNG SHALL have a recorded path, byte size, media type, and SHA-256 digest; the inventory SHALL not infer that it represents the complete upload corpus.

#### Scenario: A later migration requires reconciliation
- **WHEN** a future migration is authorized and target counts or file hashes differ from the recorded source baseline
- **THEN** operators can compare against that source evidence and keep the source independently available for rollback