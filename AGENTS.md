# CopyStart Agent Rules

CopyStart is being rebuilt from a single-company ASP.NET Core 6 MVC prototype into a multi-tenant SaaS for small repair and technical-service businesses.

## Read First

- [CONTEXT.md](CONTEXT.md): current verified state and technology baseline.
- [SYSTEM.md](SYSTEM.md): delivery rules and Definition of Done.
- [docs/README.md](docs/README.md): architecture, decisions, and workflow guides.
- `openspec/changes/`: planned behavior. OpenSpec is the source of truth for changes.

## Non-Negotiable Rules

- Plan and implement through OpenSpec. Update the affected change artifacts before any post-apply code change.
- Apply changes in the order recorded in [docs/delivery/roadmap.md](docs/delivery/roadmap.md). Do not skip a prerequisite gate.
- Treat `Application/CopyStart` as legacy discovery evidence, not as the target architecture.
- Never commit, print, or log secrets, tokens, connection strings, or presigned URLs.
- Tenant context comes only from validated server-side identity or verified host mapping. Missing or conflicting context fails closed.
- GovCore (`Tramites`) is a pattern source. Reuse verified patterns and tests, never government terminology, GOV.CO assets, or its permissive tenant fallbacks.
- Infrastructure changes also follow the mini-cluster Day-1 contract (`mini-cluster/AGENTS.md`).

## Language

- Technical artifacts, code, and commit messages: English.
- Business concepts may keep Spanish product vocabulary only in tenant templates and UI copy.
