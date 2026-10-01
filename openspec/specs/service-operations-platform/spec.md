## Purpose

Provide the canonical service-business domain, supported .NET 10 modular backend architecture, audited dependency baseline, and typed workflow invariants for requests, work orders, customers, service catalogs, and optional modules.

## Requirements

### Requirement: Supported modular backend
The platform MUST run on a supported .NET 10 LTS runtime and SHALL separate Domain, Application, Infrastructure, API, unit-test, and end-to-end-test responsibilities. Direct runtime, framework, ORM, and database-provider dependencies MUST use supported versions compatible with the target framework and with one another.

#### Scenario: Domain behavior is tested
- **WHEN** a work-order transition is exercised in a unit test
- **THEN** it runs without HTTP, database, filesystem, or identity-provider dependencies

#### Scenario: A runtime dependency reaches end of support
- **WHEN** the target dependency inventory identifies an unsupported runtime, framework, ORM, provider, or library
- **THEN** it is upgraded, replaced, or removed before the corresponding target is released, with compatibility verified by restore, build, and tests

### Requirement: Dependency graph is audited and intentional
Every direct and transitive dependency in the deployable backend MUST have a documented disposition, and the resolved target graph MUST have no unaccepted critical or high-severity known vulnerabilities. Unused or incompatible database providers and obsolete tooling MUST NOT be retained in the target merely because they exist in the prototype.

#### Scenario: The legacy graph contains multiple database providers
- **WHEN** dependency analysis finds a provider not used by the target persistence design
- **THEN** it is removed unless a documented target capability requires it

#### Scenario: Vulnerability scan finds a blocking advisory
- **WHEN** the target dependency graph contains a critical or high-severity advisory with an available fix
- **THEN** restore/build validation fails until the dependency is fixed or a time-bounded, reviewed exception is recorded

#### Scenario: No compatible fix exists
- **WHEN** a critical or high-severity advisory has no compatible remediation
- **THEN** the affected package or feature is replaced, isolated from release, or explicitly blocked pending a security decision

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

### Requirement: Conditional, rehearsed legacy migration
Legacy data import and cutover SHALL be planned only when an authorized owner identifies an authoritative immutable source and approves preservation/import. When performed, migration MUST use explicit mappings, reconciliation reports, tenant ownership rules, and a documented cutover and rollback strategy. Absence of a legacy source SHALL NOT block backend scaffolding, domain behavior, or dependency remediation.

#### Scenario: Migrated totals do not reconcile
- **WHEN** target counts or relationship checks differ from the source baseline
- **THEN** cutover is blocked and the legacy source remains authoritative

#### Scenario: No authoritative source is identified
- **WHEN** the owner cannot identify an authoritative database or file snapshot for migration
- **THEN** import is recorded as not applicable and backend implementation continues without querying unrelated infrastructure
