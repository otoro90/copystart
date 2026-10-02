# saas-platform-prerequisites Specification

## Purpose
TBD - created by archiving change provision-saas-platform-prerequisites. Update Purpose after archive.
## Requirements
### Requirement: Runtime prerequisites have an explicit application contract
CopyStart SHALL define and validate the application-facing PostgreSQL, private object-storage, and OIDC configuration contract. Platform resources and injected secret values MUST remain owned by the mini-cluster repository.

#### Scenario: Runtime configuration is reviewed
- **WHEN** CopyStart documents or changes a required platform setting
- **THEN** its name, purpose, sensitivity, source reference, and consuming readiness check are explicit and agree with the mini-cluster contract

### Requirement: Runtime secrets are injected without repository values
Database passwords, S3 credentials, and OIDC client secrets MUST be supplied through protected runtime references and MUST NOT be committed, embedded in images, logged, or accepted in client request payloads.

#### Scenario: Secret reference is absent
- **WHEN** a required credential is missing from the runtime environment
- **THEN** the application fails startup or readiness closed and does not fall back to anonymous access or a checked-in value

### Requirement: Tenant-owned data access is isolated
Every request that reads or writes tenant-owned database rows or objects MUST use validated server-side tenant context and prevent access to another tenant's data.

#### Scenario: Tenant requests another tenant's object
- **WHEN** an authenticated tenant requests an object not owned by its resolved server-side tenant context
- **THEN** the application denies access without disclosing object metadata

### Requirement: Dependency readiness fails closed
The application SHALL expose dependency-specific readiness for required PostgreSQL, OIDC metadata, and private object storage without treating a network connection alone as authorization success.

#### Scenario: A required dependency is unavailable or unauthorized
- **WHEN** a configured dependency cannot be reached or authenticated
- **THEN** readiness reports that dependency as unavailable without revealing secret values or marking the application ready

### Requirement: Production configuration remains gated
Production credentials and production workload activation MUST remain absent/disabled until a separately approved production cutover.

#### Scenario: Development release is prepared
- **WHEN** a development image or overlay is promoted
- **THEN** it cannot populate production secrets or activate the production workload

