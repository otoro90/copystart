## ADDED Requirements

### Requirement: Supported modular backend
The platform MUST run on a supported .NET 10 LTS runtime and SHALL separate Domain, Application, Infrastructure, API, unit-test, and end-to-end-test responsibilities.

#### Scenario: Domain behavior is tested
- **WHEN** a work-order transition is exercised in a unit test
- **THEN** it runs without HTTP, database, filesystem, or identity-provider dependencies

### Requirement: Canonical service operations model
The platform SHALL represent parties, service requests, work orders, timeline events, attachments, service catalogs, and optional asset, procedure, and part modules using stable canonical concepts.

#### Scenario: A business does not manage physical assets
- **WHEN** its enabled modules exclude asset management
- **THEN** it can create and complete work requests without a synthetic asset record

### Requirement: Typed workflow invariants
Work-request and work-order states and transitions MUST be typed, validated in the domain/application boundary, audited, and persisted atomically.

#### Scenario: An invalid transition is requested
- **WHEN** a completed work order is asked to return directly to an initial state
- **THEN** the command fails with a domain error and no state or audit record is changed

### Requirement: Versioned HTTP contract
The API SHALL expose versioned endpoints with OpenAPI metadata, Problem Details errors, validation, cancellation, pagination where needed, and liveness/readiness endpoints.

#### Scenario: Invalid input reaches an endpoint
- **WHEN** request validation fails
- **THEN** the API returns a documented Problem Details response without executing the use case

### Requirement: Rehearsed legacy migration
Legacy data migration MUST use explicit mappings, immutable source snapshots, reconciliation reports, and a documented cutover and rollback strategy finalized after tenant ownership is defined.

#### Scenario: Migrated totals do not reconcile
- **WHEN** target counts or relationship checks differ from the source baseline
- **THEN** cutover is blocked and the legacy source remains authoritative