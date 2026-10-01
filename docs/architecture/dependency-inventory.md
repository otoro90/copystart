# Target and Legacy Dependency Inventory

Verified: 2026-09-30.

## 1. Legacy Prototype Dependency Disposition

The legacy prototype at `Application/CopyStart` targets out-of-support .NET 6 (`net6.0`). Each package reference in `Application/CopyStart/CopyStart.csproj` has been audited against actual codebase usage and the target SaaS architecture:

| Legacy Package | Legacy Version | Disposition | Rationale & Evidence |
| --- | --- | --- | --- |
| `Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore` | 6.0.9 | Remove | EF Core diagnostics for MVC developer exception page are not needed for the headless API; built-in ASP.NET Core developer exception page and RFC 7807 Problem Details are used. |
| `Microsoft.AspNetCore.Identity.EntityFrameworkCore` | 6.0.9 | Remove | Local cookie/database ASP.NET Identity is retired. Federated identity via ZITADEL is the target architecture ([ADR-0003](decisions/0003-tenant-identity.md)). |
| `Microsoft.AspNetCore.Identity.UI` | 6.0.9 | Remove | Razor UI for identity pages is retired with the MVC front-end. |
| `Microsoft.EntityFrameworkCore.SqlServer` | 6.0.9 | Remove | Audit of the prototype codebase confirms zero calls to `UseSqlServer`. The prototype exclusively configured `UseNpgsql` in `Startup.cs`. SQL Server is not part of the target platform. |
| `Microsoft.EntityFrameworkCore.Tools` | 6.0.9 | Upgrade / Replace | Replaced by `Microsoft.EntityFrameworkCore.Design` 10.0.12 in the target API project for CLI tooling. |
| `Microsoft.VisualStudio.Web.CodeGeneration.Design` | 6.0.9 | Remove | MVC scaffolding tool for Visual Studio. Not required for a clean modular monolith API. |
| `Microsoft.Web.LibraryManager.Build` | 2.1.175 | Remove | LibMan build tool used for restoring client-side assets in Razor/AdminLTE. Fails with `LIB002` on `datatables.net@1.12.1`. Classified as retire-with-Razor. |
| `Npgsql.EntityFrameworkCore.PostgreSQL` | 6.0.7 | Upgrade | PostgreSQL is the target database ([ADR-0002](decisions/0002-dotnet-modular-monolith.md)). Upgraded to supported version 10.0.3 in the target `CopyStart.Infrastructure` project. |
| `Npgsql.EntityFrameworkCore.PostgreSQL.Design` | 1.1.0 | Remove | Obsolete legacy design package deprecated since historical EF Core 2.0. No reverse-engineering or `Scaffold-DbContext` workflow exists in the repository. |

## 2. Legacy Build Diagnostics and Transitive Vulnerabilities

- **LibMan Failure (`LIB002`)**: `Application/CopyStart/libman.json` requests `datatables.net@1.12.1`, which is unresolvable from the legacy cdnjs provider. This asset path is classified as **retire-with-Razor**. The prototype remains unbuilt in CI, acting solely as a diagnostic reference.
- **Transitive Vulnerabilities in Legacy Graph**: Vulnerability audits on the legacy .NET 6 graph identified known advisories in legacy packages including Npgsql 6.x, `Microsoft.Extensions.Caching.Memory` 6.x, and older `System.*` runtime assemblies. These vulnerabilities belong to the retired prototype and are eliminated in the target by adopting .NET 10 LTS and updated direct dependencies.

## 3. Target .NET 10 Dependency Inventory

The target backend is organized as a modular monolith under `src/` using Central Package Management (`Directory.Packages.props`) and `.NET 10 SDK 10.0.103` (`global.json`).

| Target Package | Version | Layer / Project | Purpose |
| --- | --- | --- | --- |
| `Microsoft.EntityFrameworkCore` | 10.0.12 | Infrastructure | ORM core for PostgreSQL persistence mapping. |
| `Microsoft.EntityFrameworkCore.Design` | 10.0.12 | Api | Migration tooling and design-time EF support. |
| `Npgsql.EntityFrameworkCore.PostgreSQL` | 10.0.3 | Infrastructure | PostgreSQL database provider compatible with EF Core 10. |
| `WolverineFx` | 6.43.0 | Application, Api | In-process command/query dispatching and middleware pipeline. |
| `WolverineFx.FluentValidation` | 6.43.0 | Application, Api | Automatic command validation middleware executing before handlers. |
| `FluentValidation` | 12.1.1 | Application | Typed domain and command validators. |
| `FluentValidation.DependencyInjectionExtensions` | 12.1.1 | Application, Api | Validator discovery and service registration. |
| `Microsoft.AspNetCore.OpenApi` | 10.0.12 | Api | Built-in OpenAPI specification generation for HTTP endpoints. |
| `Microsoft.AspNetCore.Mvc.Testing` | 10.0.12 | E2ETests | In-memory HTTP test server for integration testing. |
| `xunit` | 2.9.3 | UnitTests, E2ETests | Test framework. |
| `xunit.runner.visualstudio` | 4.0.0 | UnitTests, E2ETests | Test runner for CLI and IDE integration. |
| `Microsoft.NET.Test.Sdk` | 18.10.1 | UnitTests, E2ETests | .NET test execution platform SDK. |

## 4. Import and Cutover Classification

As confirmed in [CONTEXT.md](../../CONTEXT.md) and [backend-foundation.md](backend-foundation.md), the owner verified that the prototype was never in production, the historical database credential is retired and was not reused, and no authoritative database dump or upload snapshot exists. Therefore, **legacy data import and cutover are classified as not applicable**. Implementation proceeds directly with clean target domain models without importing historical records.
