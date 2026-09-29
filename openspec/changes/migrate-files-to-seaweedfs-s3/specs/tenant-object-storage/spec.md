## ADDED Requirements

### Requirement: Objects remain private and tenant-scoped
All application objects MUST be private and MUST use server-generated keys scoped by environment and tenant, with authorization checked before every transfer URL is issued.

#### Scenario: User requests another tenant's file
- **WHEN** an authenticated user supplies or discovers an object identifier owned by another tenant
- **THEN** the API denies access without returning its key, metadata, or existence

### Requirement: Uploads use a verified lifecycle
The system SHALL create pending metadata and an upload intent before issuing a short-lived presigned URL, then verify object size, content type, checksum, and scan result before making the object available.

#### Scenario: Uploaded content fails verification
- **WHEN** the stored object differs from declared constraints or fails malware scanning
- **THEN** it remains quarantined, cannot be downloaded by users, and records an auditable failure reason

### Requirement: Storage endpoints serve distinct trust boundaries
Backend object operations MUST use the internal SeaweedFS endpoint, while browser-facing signatures MUST use the canonical external HTTPS endpoint with path-style addressing and restricted CORS.

#### Scenario: Browser uploads directly
- **WHEN** an authorized upload intent is created
- **THEN** the returned URL is externally resolvable, short-lived, limited to the intended object and operation, and valid only from an approved origin

### Requirement: Metadata and object lifecycle reconcile
Deletion, retention, orphan cleanup, and failed uploads SHALL be idempotent and MUST reconcile PostgreSQL metadata with SeaweedFS state without treating one partial operation as complete.

#### Scenario: Database commit succeeds but object upload never occurs
- **WHEN** the pending upload expires
- **THEN** cleanup marks or removes the orphan metadata safely and does not affect another tenant's object

### Requirement: Legacy files migrate resumably
Legacy filesystem migration MUST use a manifest with source path, target key, size, checksum, tenant owner, status, and retry history.

#### Scenario: Migration stops midway
- **WHEN** the migration is rerun from the same manifest
- **THEN** verified objects are skipped and remaining objects resume without duplicate available records