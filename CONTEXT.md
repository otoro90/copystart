# CONTEXT — Verified Current State

Last verified: 2026-09-30.

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
| Workflow | String states assigned in controllers (`Por tramitar`, `Asignada`, `En ejecucion`, `Finalizado`) |
| Files | Local filesystem via `PathBaseFiles` |
| UI | Razor + AdminLTE with hard-coded Copy Start branding |
| Security debt | PostgreSQL credential values were tracked in prototype configuration and Git history. Current configuration has been cleaned; the owner confirms the prototype is retired, was never production, and the credential was not reused. Treat the historical value as compromised and never reuse it. |

The prototype is product-discovery evidence only. No authoritative database dump or external upload snapshot is available, and no tenant-owned SaaS data is in scope. The new SaaS starts without importing prototype records; migration inventory is not applicable. The sole tracked file under `Application/CopyStart/filesystem/archivos/` is documented in [the legacy inventory report](docs/security/legacy-inventory.md), and does not represent a complete upload corpus.

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
- Agent skills are the OpenSpec skills only. Rejected candidates are recorded in [docs/agent-workflows/skill-candidates.md](docs/agent-workflows/skill-candidates.md).

## Known Blockers

- The historical CopyStart PostgreSQL credential is confirmed retired and unused; rotation is not applicable. Its exposed historical value remains compromised and must never be reused. No authoritative legacy database or upload snapshot exists, so prototype-data migration is not applicable.
- Shared SeaweedFS is a separate mini-cluster concern: it still allows anonymous read and holds tracked test credentials. Storage integration remains blocked until the mini-cluster owner hardens it; this is not a CopyStart prototype data source.
- Production stays disabled until restore, isolation, and rollback evidence is approved for a named pilot.
