## Context

CopyStart targets unsupported .NET 6 and uses controllers as persistence and workflow services. `ApplicationDbContext` combines identity and business data; entities contain UI labels. Request status and work-order status are different fields. Request strings include `Por tramitar`, `Asignada`, `En servicio`, `Servicios Finalizados`, and `Cancelada`. Work-order strings are `Por confirmar`, `En ejecucion`, and `Finalizado`. Controllers assign those strings directly. The verified map is in `docs/architecture/backend-foundation.md`. GovCore patch versions cited below are a reference snapshot, not this repository's package pin. No `src/` solution exists yet. Its package list pins EF Core/ASP.NET Core 6.0.9 and Npgsql 6.0.7, includes both SQL Server and PostgreSQL providers plus an old Npgsql Design package, and has no target dependency inventory. A baseline build fails because LibMan cannot resolve `datatables.net@1.12.1`. NuGet audit reports multiple vulnerable transitive packages. GovCore currently demonstrates a .NET 10 layered solution with Wolverine, EF Core 10, PostgreSQL, API middleware, and separate test projects, but its government-specific domain and permissive tenant behavior are not reusable.

## Goals / Non-Goals

**Goals:**
- Build a supported modular monolith and preserve validated service-business knowledge.
- Replace the unsupported and vulnerable legacy dependency graph with a minimal, supported, compatible target dependency set.
- Make business transitions testable and independent of transport and persistence.
- Provide stable APIs for separate web applications.
- Enable safe incremental data migration if an authoritative source is found and its owner approves an import.

**Non-Goals:**
- Preserve MVC/Razor compatibility.
- Build microservices or introduce persistent messaging without a concrete external side effect.
- Finalize tenant-owned schema before the tenant identity design is accepted.

## Decisions

1. Target .NET 10 LTS and current patches. Microsoft lists .NET 10 as active LTS through November 14, 2028; .NET 6 ended support in November 2024.
2. Build a dependency inventory before changing the legacy package graph. For every direct/transitive package record current version, target use, support/vulnerability status, and disposition: retain, upgrade, replace, or remove. This is code/dependency evidence and does not require a legacy data snapshot. `dotnet list package --outdated --include-transitive` is evidence, not an instruction to move every package to its numerically newest release.
3. Use exact, mutually compatible stable package versions for the target runtime. Centralize versions in `Directory.Packages.props` once the multi-project solution exists; keep SDK selection in `global.json` with an explicitly reviewed roll-forward policy.
4. Remove `Microsoft.EntityFrameworkCore.SqlServer` and `Npgsql.EntityFrameworkCore.PostgreSQL.Design` only after confirming no target code-generation workflow needs them. Align Npgsql EF provider major version with EF Core and test the actual PostgreSQL provider. Do not carry legacy Design/LibMan dependencies forward solely for MVC scaffolding.
5. Resolve LibMan/DataTables separately from package upgrades: if the target public/operations apps do not use it, retire the legacy asset path with Razor/AdminLTE; if required during an interim runnable phase, pin and test an available source instead of silently ignoring restore failure.
6. Use a modular monolith with Domain, Application, Infrastructure, API, UnitTests, and E2ETests projects. This matches the useful GovCore boundary while reducing operational complexity for a small SaaS.
7. Use Wolverine for in-process command/query dispatch and pipeline behaviors, normal scoped DI for repositories and `ApplicationDbContext`, and an explicit unit-of-work transaction boundary. Do not enable an outbox until an external effect needs durable at-least-once delivery.
8. Separate the operations core from optional modules. `Customer`, `WorkRequest`, `WorkOrder`, and timeline are core; assets, parts, field dispatch, recurring maintenance, SLA, and billing are modules.
9. Replace state strings with enums/value objects and transition methods. Preserve legacy values only in migration mappings.
10. Use controller-based versioned APIs, built-in OpenAPI, Problem Details, authorization policies, rate limiting on abuse-prone public endpoints, and separate live/ready health checks.
11. Use a strangler migration only if the data owner identifies an authoritative, immutable source snapshot and approves preservation/import. Then create the new API/schema, rehearse and reconcile imports, freeze source writes for final import, switch clients, and retain rollback to the preserved source. If no source is identified, record import as not applicable; this does not block the backend skeleton, domain work, or dependency remediation. Dual-write is rejected because the old controllers cannot guarantee equivalent invariants.
12. Require CI to restore, build, test, and audit the target graph at each migration step. CI begins with diagnostic coverage of the legacy solution, then gates each newly introduced target; see `establish-github-ci-and-image-publishing`.

## Risks / Trade-offs

- [Prematurely generic domain loses real workflows] -> Validate every aggregate against copier-service and automotive scenarios.
- [Layering becomes ceremony] -> Keep modules vertical and avoid repositories or abstractions without a real boundary.
- [Blind major-version bumps cause incompatibilities] -> Upgrade from the framework/provider compatibility matrix, use exact versions, and validate with clean restore, build, integration tests, and migration checks.
- [Legacy LibMan failure blocks a useful baseline] -> Determine whether the legacy app must remain runnable; retire its asset pipeline if replaced, otherwise repair it as a separately tracked compatibility step.
- [Transitive vulnerabilities cannot be upgraded independently] -> Upgrade or replace the owning direct package; permit only reviewed, time-bounded exceptions.
- [Tenant design changes uniqueness and foreign keys] -> Do not finalize persistence or migration scripts before `tenant-identity-access` is accepted.
- [No authoritative legacy data source is available] -> Record import as not applicable; do not synthesize or discover unrelated cluster data, and continue application modernization.
- [An approved source contains data-quality issues] -> Use an inventory baseline, explicit exception reports, and repeatable dry runs before any cutover.

## Migration Plan

1. Verify governance and local prototype credential-remediation prerequisites; check whether an owner-approved legacy source exists, without blocking the target work if it does not.
2. Capture the current dependency/vulnerability inventory and classify the legacy build failure.
3. Create the solution skeleton, pin the .NET 10 SDK, centralize target package versions, and add architecture tests.
4. Add only required .NET 10, EF Core 10, Npgsql 10, Wolverine, validation, OpenAPI, and test packages at compatible supported patches; remove unused providers/tooling.
5. Run clean restore, build, tests, and vulnerability audit; record and review every exception before domain implementation.
6. Model canonical operations and typed transitions with unit tests.
7. Accept the tenant ownership and capability-registry contracts.
8. Add EF Core mappings, API endpoints, integration tests, and health checks.
9. If an authoritative source snapshot is approved, build mappings and rehearse imports; otherwise record migration as not applicable.
10. Freeze source writes, take a final snapshot, import, reconcile, and cut over only after an explicit migration decision.

Rollback redirects users to the preserved legacy application and restores the pre-cutover target snapshot. Imported target data is not written back to the legacy schema.

## Research Evidence

- Microsoft .NET support policy, accessed 2026-09-27: .NET 10 is active LTS through 2028; .NET 6 is out of support.
- ASP.NET Core 10 official documentation via Context7, accessed 2026-09-27: built-in JWT/OIDC authentication, authorization, OpenAPI, health checks, rate limiting, and Problem Details support.
- Local GovCore project files, 2026-09-27: .NET 10, EF Core 10.0.11, Npgsql 10.0.3, and Wolverine 6.32.0.
- Local CopyStart inspection, 2026-09-27: direct `DbContext` use and string state transitions in MVC controllers.
- Local project/package inventory, 2026-09-30: .NET SDK 10.0.103 reports the prototype `net6.0` target as unsupported; direct versions include ASP.NET Core/EF Core 6.0.9 and Npgsql 6.0.7. The legacy graph, not a .NET 10 graph, was the one reported with vulnerable transitive packages including Npgsql, Microsoft.Extensions.Caching.Memory, MessagePack, and older System.* packages. The new solution has not been restored.
- Local build, 2026-09-30: `dotnet build CopyStart.sln --no-restore` fails with `LIB002` because LibMan cannot resolve `datatables.net@1.12.1`; this is a legacy UI asset restore failure, not proof that the target backend design fails.
- NuGet stable feed, 2026-09-30: EF Core 10.0.12, Npgsql EF provider 10.0.3, WolverineFx 6.43.0, and FluentValidation 12.1.1 were available. These are feed evidence, not adopted pins. Re-check before writing `Directory.Packages.props`.

## Open Questions

- Resolved 2026-09-30: the owner did not identify an authoritative legacy database or file snapshot. Import is not applicable. See `CONTEXT.md`.
- Which workflows from the copier business are still used today?
- Should work-order numbering be tenant-configurable while retaining a stable internal identifier?