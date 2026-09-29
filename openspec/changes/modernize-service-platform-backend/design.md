## Context

CopyStart targets unsupported .NET 6 and uses controllers as persistence and workflow services. `ApplicationDbContext` combines identity and business data; entities contain UI labels; status strings such as `Por tramitar`, `En ejecucion`, and `Finalizado` are assigned directly by controllers. GovCore currently demonstrates a .NET 10 layered solution with Wolverine, EF Core 10, PostgreSQL, API middleware, and separate test projects, but its government-specific domain and permissive tenant behavior are not reusable.

## Goals / Non-Goals

**Goals:**
- Build a supported modular monolith and preserve validated service-business knowledge.
- Make business transitions testable and independent of transport and persistence.
- Provide stable APIs for separate web applications.
- Enable safe incremental data migration.

**Non-Goals:**
- Preserve MVC/Razor compatibility.
- Build microservices or introduce persistent messaging without a concrete external side effect.
- Finalize tenant-owned schema before the tenant identity design is accepted.

## Decisions

1. Target .NET 10 LTS and current patches. Microsoft lists .NET 10 as active LTS through November 14, 2028; .NET 6 ended support in November 2024.
2. Use a modular monolith with Domain, Application, Infrastructure, API, UnitTests, and E2ETests projects. This matches the useful GovCore boundary while reducing operational complexity for a small SaaS.
3. Use Wolverine for in-process command/query dispatch and pipeline behaviors, normal scoped DI for repositories and `ApplicationDbContext`, and an explicit unit-of-work transaction boundary. Do not enable an outbox until an external effect needs durable at-least-once delivery.
4. Separate the operations core from optional modules. `Customer`, `WorkRequest`, `WorkOrder`, and timeline are core; assets, parts, field dispatch, recurring maintenance, SLA, and billing are modules.
5. Replace state strings with enums/value objects and transition methods. Preserve legacy values only in migration mappings.
6. Use controller-based versioned APIs, built-in OpenAPI, Problem Details, authorization policies, rate limiting on abuse-prone public endpoints, and separate live/ready health checks.
7. Use a strangler migration: create the new API and schema, import snapshots in rehearsals, reconcile, freeze legacy writes for final import, switch clients, and retain rollback to the read-only legacy system. Dual-write is rejected because the old controllers cannot guarantee equivalent invariants.

## Risks / Trade-offs

- [Prematurely generic domain loses real workflows] -> Validate every aggregate against copier-service and automotive scenarios.
- [Layering becomes ceremony] -> Keep modules vertical and avoid repositories or abstractions without a real boundary.
- [Tenant design changes uniqueness and foreign keys] -> Do not finalize persistence or migration scripts before `tenant-identity-access` is accepted.
- [Legacy data quality blocks import] -> Use the inventory baseline, explicit exception reports, and repeatable dry runs.

## Migration Plan

1. Create the solution skeleton and architecture tests.
2. Model canonical operations and typed transitions with unit tests.
3. Accept the tenant ownership and capability-registry contracts.
4. Add EF Core mappings, API endpoints, integration tests, and health checks.
5. Build migration mappings and run repeatable rehearsal imports.
6. Freeze legacy writes, take a final snapshot, import, reconcile, and cut over.

Rollback redirects users to the preserved legacy application and restores the pre-cutover target snapshot. Imported target data is not written back to the legacy schema.

## Research Evidence

- Microsoft .NET support policy, accessed 2026-09-27: .NET 10 is active LTS through 2028; .NET 6 is out of support.
- ASP.NET Core 10 official documentation via Context7, accessed 2026-09-27: built-in JWT/OIDC authentication, authorization, OpenAPI, health checks, rate limiting, and Problem Details support.
- Local GovCore project files, 2026-09-27: .NET 10, EF Core 10.0.11, Npgsql 10.0.3, and Wolverine 6.32.0.
- Local CopyStart inspection, 2026-09-27: direct `DbContext` use and string state transitions in MVC controllers.

## Open Questions

- Which legacy database snapshot is authoritative?
- Which workflows from the copier business are still used today?
- Should work-order numbering be tenant-configurable while retaining a stable internal identifier?