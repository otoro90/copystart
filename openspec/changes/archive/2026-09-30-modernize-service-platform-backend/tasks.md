## 1. Architecture Foundation

- [x] 1.1 Confirm the prerequisite record in `CONTEXT.md` and `docs/architecture/backend-foundation.md`: governance and credential remediation are archived, and no owner-approved legacy source exists. Do not ask the owner again unless those documents change.
- [x] 1.2 Inventory direct and transitive packages with outdated/vulnerable status and a retain/upgrade/replace/remove decision for each
- [x] 1.3 Keep the recorded retire-with-Razor classification for the LibMan `datatables.net@1.12.1` / `LIB002` failure. Leave the prototype diagnostic in place and do not repair LibMan for the target.
- [x] 1.4 Use the source check in `docs/architecture/backend-foundation.md`: the prototype has no `UseSqlServer` or scaffold workflow. Do not add SQL Server, `Npgsql.EntityFrameworkCore.PostgreSQL.Design`, CodeGeneration, or LibMan to the target.
- [x] 1.5 Scaffold .NET 10 Domain, Application, Infrastructure, API, UnitTests, and E2ETests projects
- [x] 1.6 Pin the supported .NET 10 SDK and centralize exact compatible package versions for the multi-project target
- [x] 1.7 Add architecture tests enforcing dependency direction and API boundary rules
- [x] 1.8 Configure PostgreSQL, EF Core migrations, Wolverine dispatch, validation, unit of work, Problem Details, OpenAPI, and health checks
- [x] 1.9 Require clean restore, format, build, unit tests, and package vulnerability audit before the target foundation is accepted

## 2. Canonical Operations Domain

- [x] 2.1 Write failing tests for request and work-order lifecycle invariants derived from the legacy workflows
- [x] 2.2 Implement parties, requests, work orders, timeline events, and service catalogs
- [x] 2.3 Implement optional asset, procedure, and parts module boundaries
- [x] 2.4 Add typed commands, queries, authorization requirements, auditing, and versioned API endpoints
- [x] 2.5 Verify invalid transitions are atomic and produce documented errors

## 3. Legacy Migration and Validation

- [x] 3.1 Freeze the target persistence mapping until the tenant ownership design is accepted
- [x] 3.2 If an authoritative, immutable source snapshot is located and approved, build explicit mappings and repeatable dry-run import tooling; otherwise record import as not applicable
- [x] 3.3 For an approved source only, reconcile counts, relationships, states, and representative histories in an isolated target database
- [x] 3.4 Document write freeze, cutover, rollback, and source-retention procedures only for an approved import; do not make production cutover a prerequisite to backend completion
- [x] 3.5 Run unit, integration, architecture, API-contract, migration, and target dependency audit gates
- [x] 3.6 Run strict OpenSpec validation