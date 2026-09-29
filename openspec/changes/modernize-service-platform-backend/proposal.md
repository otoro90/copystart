## Why

The existing .NET 6 MVC application embeds persistence, authorization, workflow transitions, and presentation in controllers and entities, while .NET 6 is out of support. A supported, testable API foundation is required before CopyStart can safely become a multi-tenant SaaS.

## What Changes

- **BREAKING** Replace the executable MVC monolith with a .NET 10 modular monolith organized into Domain, Application, Infrastructure, API, UnitTests, and E2ETests projects.
- Preserve and refine the business concepts represented by customers, assets, requests, work orders, procedures, parts, and attachments.
- Replace string-based states and controller mutations with typed commands, queries, domain transitions, validation, and transaction boundaries.
- Expose versioned HTTP APIs with Problem Details, OpenAPI, health checks, auditing, and consistent result handling.
- Define an incremental legacy-data migration and reconciliation path rather than sharing the old schema indefinitely.

## Capabilities

### New Capabilities
- `service-operations-platform`: Provides the canonical service-business domain and supported API architecture for requests, work orders, assets, procedures, parts, customers, and history.

### Modified Capabilities

None.

## Impact

Replaces the runtime structure under `Application/CopyStart` while using its entities and workflows as discovery evidence. Introduces .NET 10, EF Core 10, PostgreSQL, Wolverine-style command/query handling, validation, OpenAPI, and automated tests. Depends on `establish-project-governance`.