## 1. Architecture Foundation

- [ ] 1.1 Verify prerequisite governance and legacy inventory artifacts are complete
- [ ] 1.2 Scaffold .NET 10 Domain, Application, Infrastructure, API, UnitTests, and E2ETests projects
- [ ] 1.3 Add architecture tests enforcing dependency direction and API boundary rules
- [ ] 1.4 Configure PostgreSQL, EF Core migrations, Wolverine dispatch, validation, unit of work, Problem Details, OpenAPI, and health checks

## 2. Canonical Operations Domain

- [ ] 2.1 Write failing tests for request and work-order lifecycle invariants derived from the legacy workflows
- [ ] 2.2 Implement parties, requests, work orders, timeline events, and service catalogs
- [ ] 2.3 Implement optional asset, procedure, and parts module boundaries
- [ ] 2.4 Add typed commands, queries, authorization requirements, auditing, and versioned API endpoints
- [ ] 2.5 Verify invalid transitions are atomic and produce documented errors

## 3. Legacy Migration and Validation

- [ ] 3.1 Freeze the target persistence mapping until the tenant ownership design is accepted
- [ ] 3.2 Build explicit legacy mappings and repeatable dry-run import tooling from immutable snapshots
- [ ] 3.3 Reconcile counts, relationships, states, and representative histories in an isolated target database
- [ ] 3.4 Document write freeze, cutover, rollback, and source-retention procedures
- [ ] 3.5 Run unit, integration, architecture, API-contract, migration, and strict OpenSpec validation