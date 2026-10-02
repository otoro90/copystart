# CONTEXT — Verified Current State

Last verified: 2026-10-02.

## Product

- Multi-tenant SaaS for repair and technical-service businesses: requests, work orders, assets, procedures, parts, attachments, and history.
- First pilots: a photocopier service business (origin of the prototype) and an automotive workshop.
- Each tenant gets a branded public site, a customer portal, and a staff console, configured from versioned vertical templates.
- Working name: CopyStart. A public brand replacement is undecided and is not a delivery gate.

## Legacy Prototype (discovery evidence only)

| Item | State |
| --- | --- |
| Runtime | .NET 6 MVC (`net6.0`), out of support since 2024-11-12 |
| Identity | Local ASP.NET Identity with roles Administrador, Coordinador, Tecnico, Cliente |
| Data | Single `ApplicationDbContext`, PostgreSQL, no tenant ownership |
| Workflow | Two controller-assigned string fields. Request: `Por tramitar`, `Asignada`, `En servicio`, `Servicios Finalizados`, `Cancelada`. Work order: `Por confirmar`, `En ejecucion`, `Finalizado`. |
| Files | Local filesystem via `PathBaseFiles` |
| UI | Razor + AdminLTE with hard-coded Copy Start branding |
| Security debt | PostgreSQL credential values were tracked in prototype configuration and Git history. Current configuration has been cleaned; the owner confirms the prototype is retired, was never production, and the credential was not reused. Treat the historical value as compromised and never reuse it. |

The prototype is product-discovery evidence only. No authoritative database dump or external upload snapshot is available, and no tenant-owned SaaS data is in scope. The new SaaS starts without importing prototype records; migration inventory is not applicable. Do not ask the owner again unless this section changes. The sole tracked file under `Application/CopyStart/filesystem/archivos/` is documented in [the legacy inventory report](docs/security/legacy-inventory.md), and does not represent a complete upload corpus.

`En ejecucion` and `Finalizado` are work-order states. They are not request states. The verified map, unused prototype packages, and the LibMan retire-with-Razor classification are in [the backend foundation analysis](docs/architecture/backend-foundation.md).

## Target Baseline

| Layer | Choice | Evidence |
| --- | --- | --- |
| Backend | .NET 10 LTS modular monolith, EF Core 10, Npgsql 10, Wolverine 6 | [ADR-0002](docs/architecture/decisions/0002-dotnet-modular-monolith.md) |
| Frontend | Angular 22: SSR public site + client-rendered operations app | [ADR-0001](docs/architecture/decisions/0001-angular-over-react.md) |
| Identity | ZITADEL, one organization per tenant, shared project + Project Grants | [ADR-0003](docs/architecture/decisions/0003-tenant-identity.md) |
| Files | SeaweedFS S3, private bucket, tenant-prefixed keys, presigned URLs | [ADR-0004](docs/architecture/decisions/0004-object-storage.md) |
| Runtime | mini-cluster K3s ARM64, Argo CD, OpenBao/ESO, Traefik | `mini-cluster/CONTEXT.md` |

## Repository Controls

- The Target Baseline table is the only current runtime-version source. `llms.txt` may repeat it; `python3 scripts/governance_checks.py` fails if those copies drift.
- Package pins are centrally defined in `Directory.Packages.props` and SDK version in `global.json` (10.0.103). The adopted pins are recorded in [the backend foundation analysis](docs/architecture/backend-foundation.md) and [the dependency inventory](docs/architecture/dependency-inventory.md).
- Agent skills are the OpenSpec skills in `.github/skills`, exposed to Cursor through `.cursor/skills`. Rejected candidates are recorded in [docs/agent-workflows/skill-candidates.md](docs/agent-workflows/skill-candidates.md).
- Run `openspec` from this repository root. The mini-cluster checkout is a different OpenSpec root.

## Backend modernization

`modernize-service-platform-backend` established the .NET 10 modular monolith backend (`src/` with `Domain`, `Application`, `Infrastructure`, `Api`, `UnitTests`, `E2ETests`) pinned via `global.json` (SDK 10.0.103) and `Directory.Packages.props`. Dependency disposition and LibMan retirement are recorded in [the dependency inventory](docs/architecture/dependency-inventory.md) and [the backend foundation analysis](docs/architecture/backend-foundation.md). `add-multitenancy-and-zitadel-identity` (archived 2026-10-02) established tenant ownership (`ITenantOwned`), EF query filters, composite constraints, RLS policies, `ClaimsTenantContext` supporting ZITADEL organization claims (`urn:zitadel:iam:org:id`), and hermetic in-memory test suites. `provision-saas-platform-prerequisites` (archived 2026-10-02) established strongly typed options (`PostgreSqlOptions`, `StorageOptions`, `ZitadelOptions`) and fail-closed readiness health checks. Phase 1 is complete; execution is governed by `orchestrate-roadmap-delivery` and [the master execution runbook](docs/delivery/master-execution-runbook.md).

## Known Blockers

- The historical CopyStart PostgreSQL credential is confirmed retired and unused; rotation is not applicable. Its exposed historical value remains compromised and must never be reused. No authoritative legacy database or upload snapshot exists, so prototype-data migration is not applicable.
- Shared SeaweedFS is a separate mini-cluster concern: it still allows anonymous read and holds tracked test credentials. Storage integration remains blocked until the mini-cluster owner hardens it; this is not a CopyStart prototype data source.
- Production stays disabled until restore, isolation, and rollback evidence is approved for a named pilot.
