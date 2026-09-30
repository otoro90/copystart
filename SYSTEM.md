# SYSTEM — Delivery Rules

## Authority Order

1. `openspec/changes/<change>/` for planned behavior.
2. `CONTEXT.md` for verified current state.
3. `docs/architecture/decisions/` for accepted decisions.
4. `docs/` topic guides for detail.

When sources disagree, fix the stale source in the same change. Do not copy versions from GovCore documentation without checking its project files.

## Definition of Done

A task is complete only when all applicable items have fresh evidence:

- Behavior-focused tests written first and passing.
- Build, lint, and type checks passing.
- Tenant-isolation tests for any tenant-owned data path.
- `openspec validate <change> --strict` passing.
- `CONTEXT.md` or the relevant `docs/` guide updated.
- Migration, rollback, and secret-handling notes when data or infrastructure changes.

## Research Rule

Material or version-sensitive decisions record: local evidence, official source with access date and version, alternatives, falsifiable checks, and reevaluation triggers. Use Context7 first for library facts, then official web documentation.

## Agent Customization Rule

- Project conventions live in `AGENTS.md` and `.github/instructions/`.
- A skill is created only after a baseline pressure scenario shows agents fail without it. Record the scenario in [docs/agent-workflows/skill-candidates.md](docs/agent-workflows/skill-candidates.md).
