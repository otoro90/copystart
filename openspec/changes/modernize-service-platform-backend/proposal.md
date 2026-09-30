## Why

The existing .NET 6 MVC application embeds persistence, authorization, workflow transitions, and presentation in controllers and entities; its runtime is out of support and its dependency graph contains reported vulnerabilities. A supported, reproducible .NET 10 API foundation with an audited, purpose-aligned dependency set is required before CopyStart can safely become a multi-tenant SaaS.

## What Changes

- **BREAKING** Replace the executable MVC monolith with a .NET 10 modular monolith organized into Domain, Application, Infrastructure, API, UnitTests, and E2ETests projects.
- Inventory direct and transitive dependencies, classify each as retain, upgrade, replace, or remove, and migrate only dependencies required by the target architecture to supported compatible versions.
- Remove obsolete or unused providers and tooling after checking source and generated-code usage; do not blindly upgrade every package in the legacy MVC project.
- Replace the unsupported LibMan/DataTables dependency path when still needed by the new applications, or retire it with the Razor/AdminLTE UI.
- Require reproducible restore, build, tests, and vulnerability checks as package changes are introduced.
- Preserve and refine the business concepts represented by customers, assets, requests, work orders, procedures, parts, and attachments.
- Replace string-based states and controller mutations with typed commands, queries, domain transitions, validation, and transaction boundaries.
- Expose versioned HTTP APIs with Problem Details, OpenAPI, health checks, auditing, and consistent result handling.
- Define an incremental legacy-data migration and reconciliation path only if an authoritative, owner-approved source snapshot is found; otherwise document that import is not applicable.

## Capabilities

### New Capabilities
- `service-operations-platform`: Provides the canonical service-business domain and supported API architecture for requests, work orders, assets, procedures, parts, customers, and history.

### Modified Capabilities

None.

## Impact

Replaces the runtime structure under `Application/CopyStart` while using its entities and workflows as discovery evidence. Introduces .NET 10, EF Core 10, Npgsql 10, PostgreSQL, Wolverine-style command/query handling, validation, OpenAPI, dependency-audit gates, and automated tests. The historical dependency graph is audited as migration input, not upgraded wholesale. Depends on `establish-project-governance` and the local configuration remediation in `remediate-legacy-secrets-and-inventory`; coordinates with `establish-github-ci-and-image-publishing`. Backend scaffolding, domain work, and dependency remediation do not wait for a legacy-data inventory; import and cutover tasks remain conditional on locating and approving an authoritative snapshot.