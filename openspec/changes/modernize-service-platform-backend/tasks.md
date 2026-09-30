## 1. Architecture Foundation

- [ ] 1.1 Verify governance and local prototype credential-remediation prerequisites; ask whether an owner-approved legacy data source exists, but do not block scaffolding or dependency audit if none is found
- [ ] 1.2 Inventory direct and transitive packages with outdated/vulnerable status and a retain/upgrade/replace/remove decision for each
- [ ] 1.3 Confirm whether the legacy MVC app must remain runnable; classify the LibMan DataTables failure as retire-with-Razor or repair-for-interim-use
- [ ] 1.4 Verify source/generator references before removing SQL Server provider, old Npgsql Design, or legacy code-generation/LibMan dependencies
- [ ] 1.5 Scaffold .NET 10 Domain, Application, Infrastructure, API, UnitTests, and E2ETests projects
- [ ] 1.6 Pin the supported .NET 10 SDK and centralize exact compatible package versions for the multi-project target
- [ ] 1.7 Add architecture tests enforcing dependency direction and API boundary rules
- [ ] 1.8 Configure PostgreSQL, EF Core migrations, Wolverine dispatch, validation, unit of work, Problem Details, OpenAPI, and health checks
- [ ] 1.9 Require clean restore, format, build, unit tests, and package vulnerability audit before the target foundation is accepted

## 2. Canonical Operations Domain

- [ ] 2.1 Write failing tests for request and work-order lifecycle invariants derived from the legacy workflows
- [ ] 2.2 Implement parties, requests, work orders, timeline events, and service catalogs
- [ ] 2.3 Implement optional asset, procedure, and parts module boundaries
- [ ] 2.4 Add typed commands, queries, authorization requirements, auditing, and versioned API endpoints
- [ ] 2.5 Verify invalid transitions are atomic and produce documented errors

## 3. Legacy Migration and Validation

- [ ] 3.1 Freeze the target persistence mapping until the tenant ownership design is accepted
- [ ] 3.2 If an authoritative, immutable source snapshot is located and approved, build explicit mappings and repeatable dry-run import tooling; otherwise record import as not applicable
- [ ] 3.3 For an approved source only, reconcile counts, relationships, states, and representative histories in an isolated target database
- [ ] 3.4 Document write freeze, cutover, rollback, and source-retention procedures only for an approved import; do not make production cutover a prerequisite to backend completion
- [ ] 3.5 Run unit, integration, architecture, API-contract, migration, and target dependency audit gates
- [ ] 3.6 Run strict OpenSpec validation