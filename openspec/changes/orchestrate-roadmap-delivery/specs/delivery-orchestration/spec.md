## ADDED Requirements

### Requirement: Four-Phase Pipeline Sequencing
The delivery orchestration SHALL enforce a strict four-phase pipeline across repositories: Phase 1 (CopyStart Backend Core: changes 5 and 6), Phase 2 (Mini-Cluster Infrastructure: changes 7 and 8), Phase 3 (CopyStart Frontend and Templates: changes 9 and 10), and Phase 4 (Integration, Entitlements, and GitOps Deployment: changes 11, 12, and 13).

#### Scenario: Advancing through phases in order
- **WHEN** an implementation agent evaluates the next roadmap gate
- **THEN** it MUST verify that all prerequisite gates in the current phase are archived before transitioning to the next phase

#### Scenario: Cross-repository handoff
- **WHEN** Phase 1 completes with changes 5 and 6 archived in `copystart`
- **THEN** the execution context switches to the `mini-cluster` repository to apply changes 7 and 8 without modifying CopyStart application code

### Requirement: In-Memory Decoupled Test Fixtures
The delivery orchestration SHALL mandate that CopyStart backend multitenancy, domain, and OIDC middleware tests run with deterministic in-memory fixtures (`TestAuthenticationHandler`, mocked OIDC tokens, isolated SQLite or transaction-scoped DbContext) without requiring a live connection to the mini-cluster, ZITADEL, or SeaweedFS during Phase 1.

#### Scenario: Running backend multitenancy tests without cluster access
- **WHEN** running unit and integration suites during implementation of change 5 or 6
- **THEN** all tests SHALL pass hermetically using mocked claims and in-memory options without throwing network or connection-refused errors

#### Scenario: Configuration contract validation
- **WHEN** verifying runtime configuration options in change 6
- **THEN** validation SHALL confirm fail-closed behavior for missing or invalid configuration values using in-memory option snapshots

### Requirement: Shared Infrastructure Blast-Radius Protection
The delivery orchestration SHALL require that all operations targeting the `mini-cluster` shared environment preserve existing workloads, especially `GovCo.Tramites`, without schema mutation, credential invalidation, or resource deletion outside the isolated `copystart` namespace and boundaries.

#### Scenario: PostgreSQL database isolation
- **WHEN** provisioning database prerequisites in change 8
- **THEN** operations SHALL only create `copystart_dev_db` and role `copystart_dev_user`, leaving `tramites_dev_db`, `tramites_prod_db`, and all unrelated roles completely untouched

#### Scenario: SeaweedFS S3 storage hardening
- **WHEN** applying S3 security policies in change 7
- **THEN** operations SHALL preserve the active `tramites-app` bucket and credentials, remove anonymous global read, and isolate CopyStart objects to the dedicated private `copystart-dev-files` bucket

#### Scenario: ZITADEL organization isolation
- **WHEN** provisioning OIDC resources in change 8
- **THEN** operations SHALL create an isolated `copystart` Organization and Project Grant, without mutating or querying Tramites administrative identities
