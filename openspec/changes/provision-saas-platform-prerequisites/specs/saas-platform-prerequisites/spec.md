## ADDED Requirements

### Requirement: Shared prerequisites are declaratively owned
Development PostgreSQL, SeaweedFS, OpenBao/ESO, and shared ZITADEL resources MUST have one declared owner, least-privilege configuration, and no plaintext secrets in Git.

#### Scenario: GitOps reconciles prerequisites
- **WHEN** the cluster is rebuilt from approved repositories and protected bootstrap inputs
- **THEN** every prerequisite reaches its declared state without an imperative undocumented resource

### Requirement: SeaweedFS is hardened before application use
The shared S3 gateway MUST default to deny, remove anonymous application-data access, eliminate tracked live/test credentials, and provide a dedicated private CopyStart bucket identity.

#### Scenario: Anonymous request targets the CopyStart bucket
- **WHEN** no valid signature or authorized temporary credential is supplied
- **THEN** SeaweedFS denies read, list, write, and delete access

### Requirement: ZITADEL ownership is split explicitly
Terraform SHALL own the shared product project, applications, roles, actions, and provisioning service identity, while runtime onboarding MAY own customer organizations and Project Grants through only that constrained identity.

#### Scenario: Shared identity provisioning is rerun
- **WHEN** the Terraform workflow executes against matching remote state
- **THEN** it produces no duplicate project, application, role, or service identity

### Requirement: Bootstrap integration is bounded and idempotent
The canonical cluster facade SHALL expose a `copystart-prerequisites` profile or module that supports preflight, dry-run, idempotent apply, check-only readiness, and documented rollback.

#### Scenario: Prerequisite profile is rerun after success
- **WHEN** no desired configuration changed
- **THEN** it performs no destructive rotation or duplicate creation and reports all checks ready

### Requirement: Backups and restore are proven
The database and object bucket MUST be included in documented backup schedules and SHALL pass a non-production restore rehearsal before workload production activation.

#### Scenario: Restore rehearsal runs
- **WHEN** operators restore the latest protected backup into isolated targets
- **THEN** schema/data checks and representative object checksums reconcile without overwriting active resources