# ADR-0002: .NET 10 Modular Monolith

- Status: Accepted.
- Date: 2026-09-27.

## Decision

Rebuild the backend as a .NET 10 LTS modular monolith with Domain, Application, Infrastructure, and API projects, EF Core 10 on PostgreSQL, and Wolverine for in-process command/query dispatch.

## Rationale

- .NET 6 is out of support; .NET 10 is LTS until 2028-11-14.
- The layering matches GovCore's working structure while avoiding microservice overhead.
- Workflow states become typed domain transitions instead of controller string assignments.

## Constraints

- No transactional outbox until a concrete external side effect needs at-least-once delivery.
- Legacy migration uses immutable snapshots, rehearsed imports, reconciliation, and a write-freeze cutover. Dual-write is rejected.

## Sources

- https://dotnet.microsoft.com/platform/support/policy/dotnet-core (accessed 2026-09-27)
- GovCore project files: net10.0, EF Core 10.0.11, Npgsql 10.0.3, WolverineFx 6.32.0.
