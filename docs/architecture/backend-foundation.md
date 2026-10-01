# Backend Foundation Analysis

Verified: 2026-09-30. Implementation for `modernize-service-platform-backend` is completed and archived.

## Current State

| Item | State |
| --- | --- |
| Change tasks | `modernize-service-platform-backend` 20/20 tasks completed; archived in `openspec/changes/archive/2026-09-30-modernize-service-platform-backend`. |
| Target solution | .NET 10 modular monolith under `src/` (`Domain`, `Application`, `Infrastructure`, `Api`, `UnitTests`, `E2ETests`), `global.json` (SDK 10.0.103), `Directory.Packages.props`, and `CopyStart.sln`. |
| Legacy prototype | `Application/CopyStart` remains the unsupported .NET 6 MVC diagnostic. CI does not build or publish it. |
| Legacy import | Not applicable. The owner confirmed there is no authoritative database dump or upload snapshot. Do not ask again unless `CONTEXT.md` changes. |
| Persistence mapping | Intentionally frozen. `CopyStartDbContext` is empty without entity sets or migrations; in-memory UoW/repositories are used; readiness health check returns 503 until `add-multitenancy-and-zitadel-identity` accepts tenant ownership. |
| OpenSpec root | Run `openspec` from this repository. The mini-cluster checkout resolves a different `openspec/` tree. |

## Legacy workflow strings

Controllers assign two different status fields. They are not one sequence.

| Record | Field | Strings assigned in controllers |
| --- | --- | --- |
| Request (`Solicitud`) | `EstadoSolicitud` | `Por tramitar`, `Asignada`, `En servicio`, `Servicios Finalizados`, `Cancelada` |
| Work order (`Servicio`) | `Estado` | `Por confirmar`, `En ejecucion`, `Finalizado` |

`En ejecucion` and `Finalizado` belong to the work order. `En servicio` and `Servicios Finalizados` belong to the request. Technician availability (`Disponible`, `En servicio` on `Persona`) and asset flags (`Activo`, `Inactivo`, `Eliminado`) are separate and are not work-order states.

The target model uses typed transitions. A completed work order cannot return to its initial state. That failure must not change state or audit data.

## UI and unused legacy packages

The target does not keep MVC/Razor. `Application/CopyStart/libman.json` still requests `datatables.net@1.12.1` and fails LibMan with `LIB002`. Classification: retire that asset path with Razor. Do not repair LibMan for the new API. Leave the prototype files in place so CI can keep printing `LEGACY_DIAGNOSTIC`. The complete inventory is recorded in [the dependency inventory](dependency-inventory.md).

Checked against source on 2026-09-30:

| Legacy package | Evidence | Disposition for the new solution |
| --- | --- | --- |
| `Microsoft.EntityFrameworkCore.SqlServer` 6.0.9 | Referenced by the prototype. No `UseSqlServer` call in the prototype source. | Remove. Do not add it to the target. |
| `Npgsql.EntityFrameworkCore.PostgreSQL.Design` 1.1.0 | Obsolete design package. No `Scaffold-DbContext` workflow in the repository. | Remove. |
| `Microsoft.VisualStudio.Web.CodeGeneration.Design` 6.0.9 | MVC scaffolding only. | Remove. |
| `Microsoft.Web.LibraryManager.Build` 2.1.175 | LibMan restore for Razor/AdminLTE. | Remove. |
| `Npgsql.EntityFrameworkCore.PostgreSQL` 6.0.7 | Used by `UseNpgsql` in the prototype. Unsupported with the .NET 6 prototype. | Upgrade on the target only, to the Npgsql 10 provider below. Do not upgrade the prototype project in place. |
| ASP.NET Identity packages 6.0.9 | Local prototype identity. | Remove from the target. ZITADEL is a later change. |

The design note that a "target graph" contained vulnerable `System.*`, MessagePack, and `Microsoft.Extensions.Caching.Memory` packages describes the legacy restore, not a .NET 10 graph. The new graph does not exist until the solution is restored.

## Adopted Pins and Dependencies

Versions are centralized in `Directory.Packages.props` and pinned by SDK `10.0.103` (`global.json`):
- `Microsoft.EntityFrameworkCore` & `Microsoft.EntityFrameworkCore.Design`: `10.0.12`
- `Npgsql.EntityFrameworkCore.PostgreSQL`: `10.0.3`
- `WolverineFx`, `WolverineFx.FluentValidation`, `WolverineFx.RuntimeCompilation`: `6.43.0`
- `FluentValidation` & `FluentValidation.DependencyInjectionExtensions`: `12.1.1`
- `Microsoft.AspNetCore.OpenApi`: `10.0.12`
- `Microsoft.AspNetCore.Mvc.Testing`: `10.0.12`
- `xunit`: `2.9.3`, runner `4.0.0`, `Microsoft.NET.Test.Sdk`: `18.10.1`

## Technical Decisions and Lessons Learned

1. **Wolverine Dynamic Compilation:** Wolverine 6.x requires `WolverineFx.RuntimeCompilation` when running in `TypeLoadMode.Dynamic` without pre-generated Roslyn code. Without this package, runtime host startup fails.
2. **FluentValidation Pipeline:** Wolverine's `opts.UseFluentValidation()` executes validation before message handlers, throwing `FluentValidation.ValidationException`. In ASP.NET Core, `SuppressModelStateInvalidFilter = true` allows command payloads to pass directly into Wolverine, ensuring consistent RFC 7807 Problem Details via `ExceptionHandlingMiddleware`.
3. **Workflow Independence:** Work request states (`Por tramitar`, `Asignada`, `En servicio`, `Servicios Finalizados`, `Cancelada`) and work order states (`Por confirmar`, `En ejecucion`, `Finalizado`) are decoupled enums. Requests do not require synthetic assets.
4. **Invariant Atomicity:** Work orders in `Finalizado` cannot return to initial state; failure throws `InvalidStateTransitionException`, leaves state intact, appends no timeline event, and prevents unit-of-work commit.
5. **Persistence Freeze Contract:** `CopyStartDbContext` is empty without entity sets or migrations until tenant ownership is accepted. Readiness health checks (`/health/ready`, `/ready`) return HTTP 503 Service Unavailable to prevent premature routing.
6. **CI Integration:** Backend verification is automated via `scripts/ci/check-backend.sh` using pinned action `actions/setup-dotnet@a98b56852c35b8e3190ac28c8c2271da59106c68` (v6.0.0) with least-privilege `contents: read`.
