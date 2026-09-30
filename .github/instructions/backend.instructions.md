---
name: 'Backend .NET'
description: 'Use when creating or changing .NET backend code, domain entities, handlers, EF Core mappings, or API endpoints.'
applyTo: 'src/**/*.cs'
---
# Backend Conventions

- Target .NET 10. Projects: `Domain`, `Application`, `Infrastructure`, `Api`, `UnitTests`, `E2ETests`. Dependencies point inward to `Domain`.
- Model workflow states as enums or value objects with transition methods. Never assign status strings from controllers, because the legacy prototype did this and lost invariants.
- One command or query per use case, dispatched with Wolverine. Controllers only map HTTP to messages.
- Handlers return typed results; expected business failures become Problem Details, not exceptions.
- `DbContext` stays in `Infrastructure`. Use `IUnitOfWork` for the transaction boundary.
- No transactional outbox until an external side effect needs it.
- Write the failing domain test before implementing a transition.
