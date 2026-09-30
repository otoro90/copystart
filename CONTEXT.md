# CONTEXT — Verified Current State

Last verified: 2026-09-30.

## Product

- Multi-tenant SaaS for repair and technical-service businesses: requests, work orders, assets, procedures, parts, attachments, and history.
- First pilots: a photocopier service business (origin of the prototype) and an automotive workshop.
- Each tenant gets a branded public site, a customer portal, and a staff console, configured from versioned vertical templates.

## Legacy Prototype (discovery evidence only)

| Item | State |
| --- | --- |
| Runtime | .NET 6 MVC (`net6.0`), out of support since 2024-11-12 |
| Identity | Local ASP.NET Identity with roles Administrador, Coordinador, Tecnico, Cliente |
| Data | Single `ApplicationDbContext`, PostgreSQL, no tenant ownership |
| Workflow | String states assigned in controllers (`Por tramitar`, `Asignada`, `En ejecucion`, `Finalizado`) |
| Files | Local filesystem via `PathBaseFiles` |
| UI | Razor + AdminLTE with hard-coded Copy Start branding |
| Security debt | Database credentials tracked in `appsettings.json` and `docker-compose.yml` |

## Target Baseline

| Layer | Choice | Evidence |
| --- | --- | --- |
| Backend | .NET 10 LTS modular monolith, EF Core 10, Npgsql 10, Wolverine 6 | [ADR-0002](docs/architecture/decisions/0002-dotnet-modular-monolith.md) |
| Frontend | Angular 22: SSR public site + client-rendered operations app | [ADR-0001](docs/architecture/decisions/0001-angular-over-react.md) |
| Identity | ZITADEL, one organization per tenant, shared project + Project Grants | [ADR-0003](docs/architecture/decisions/0003-tenant-identity.md) |
| Files | SeaweedFS S3, private bucket, tenant-prefixed keys, presigned URLs | [ADR-0004](docs/architecture/decisions/0004-object-storage.md) |
| Runtime | mini-cluster K3s ARM64, Argo CD, OpenBao/ESO, Traefik | `mini-cluster/CONTEXT.md` |

## Known Blockers

- Tracked legacy credentials must be rotated before any migration work.
- Shared SeaweedFS still allows anonymous read and holds tracked test credentials; storage integration is blocked until hardened.
- Production stays disabled until restore, isolation, and rollback evidence is approved for a named pilot.
