---
name: 'Backend .NET'
description: 'Use when creating or changing .NET backend code, domain entities, handlers, EF Core mappings, or API endpoints.'
applyTo: 'src/**/*.cs'
---
# Backend Conventions

- Target .NET 10. Projects: `Domain`, `Application`, `Infrastructure`, `Api`, `UnitTests`, `E2ETests`, under `src/`. Those projects do not exist until `modernize-service-platform-backend` creates them. Dependencies point inward to `Domain`.
- Read `docs/architecture/backend-foundation.md` before mapping legacy states. Request strings and work-order strings are different fields. `En ejecucion` and `Finalizado` are work-order states.
- Model workflow states as enums or value objects with transition methods. Never assign status strings from controllers, because the legacy prototype did this and lost invariants.
- Do not add EF entity mappings or migrations before tenant ownership is accepted. Prototype import is not applicable.
- Do not copy GovCore patch versions into CopyStart. Pin exact versions only in `Directory.Packages.props`.
- Do not repair LibMan or add the prototype SQL Server provider, Npgsql Design package, or MVC scaffolding packages to the target.
- One command or query per use case, dispatched with Wolverine. Controllers only map HTTP to messages.
- Handlers return typed results; expected business failures become Problem Details, not exceptions.
- `DbContext` stays in `Infrastructure`. Use `IUnitOfWork` for the transaction boundary.
- No transactional outbox until an external side effect needs it.
- Write the failing domain test before implementing a transition.
