# GovCore Reuse Boundaries

GovCore (`Tramites/Forjanova.GovCore`) is a reference implementation. Verify current code before extracting anything.

| Reuse | Do not reuse |
| --- | --- |
| Layered project structure and Wolverine wiring | `EntidadGubernamental`, `Tramite`, NIT, and municipal fields |
| Host-subdomain tenant discovery idea | Middleware that continues when no tenant resolves |
| ZITADEL organization + Project Grant model | Legacy tenant claims (`entidadId`, `entidades`) |
| Tenant-resolution gate before rendering branded shell | GOV.CO components, CSS, and accessibility bar |
| Internal/external S3 client split | Trusting client-supplied object keys before upload |
| Tenant interceptor and state service test cases | Token logging in `auth.service.ts` |
| Dual migration SQL discipline | Government roles (`citizen`, `officer`) |

GovCore snapshot (2026-09-27): .NET 10, EF Core 10.0.11, Npgsql 10.0.3, WolverineFx 6.32.0, Angular 22.1, angular-oauth2-oidc 22. These are GovCore versions, not CopyStart's adopted patch pins. CopyStart has no package pin until `Directory.Packages.props` exists; see [the backend foundation analysis](backend-foundation.md). The GovCore `ARCHITECTURE-OVERVIEW.md` still says .NET 8 / Angular 16 and is stale.
