## Context

Before this change CopyStart contained no first-party Markdown documentation, agent rules, architecture overview, or OpenSpec root. GovCore contains valuable patterns but also stale documentation: its overview says .NET 8 and Angular 16+, while project files currently target .NET 10 and Angular 22. CopyStart therefore needs its own small authority hierarchy.

## Goals / Non-Goals

**Goals:**
- Make current product, domain, architecture, security, and delivery constraints discoverable.
- Keep OpenSpec as the source of truth for planned behavioral change.
- Add project instructions and only the skills justified by repeated workflows.

**Non-Goals:**
- Copy GovCore documentation or governmental conventions.
- Encode temporary implementation details as permanent rules.
- Create application or infrastructure behavior.

## Decisions

1. Use `CONTEXT.md` for current verified state, `SYSTEM.md` for durable delivery rules, `AGENTS.md` for concise agent routing, and `docs/` for detailed guides and ADRs.
2. Use OpenSpec for requirements, designs, and implementation tasks. A change is not complete until strict validation and fresh executable checks pass.
3. Record architectural decisions as ADRs linked from the architecture overview; include source authority, access date, decision, and reevaluation trigger.
4. Keep project-specific rules in instructions, not skills. Create a skill only for a recurring judgment-heavy workflow and test it using the RED-GREEN method from the local `writing-skills` guidance.
5. Record GovCore as a reference implementation. Reuse abstractions, tests, and lessons only after verifying the current code; never inherit GOV.CO terminology, CSS, tenant roles, or deployment identifiers.

## Risks / Trade-offs

- [Documentation drifts from code] -> Add validation ownership and require docs updates in Definition of Done.
- [Too many agent files increase context cost] -> Keep routing files concise and lazy-load topic details.
- [Copied guidance imports GovCore defects] -> Require local verification and explicit provenance for extracted patterns.

## Migration Plan

1. Populate OpenSpec context and repository authority documents.
2. Create architecture, development, testing, security, and delivery guides.
3. Add scoped instruction files.
4. Run baseline pressure scenarios before adding any custom skill, then verify changed behavior.
5. Validate links, OpenSpec health, and instruction discovery.

Rollback removes only the new governance artifacts; application runtime is unchanged.

## Research Evidence

- Local inspection, 2026-09-27: CopyStart had no project Markdown documentation before OpenSpec initialization.
- GovCore project files, 2026-09-27: .NET 10.0 and Angular 22.1 contradict the older architecture overview, demonstrating the need for explicit authority and drift checks.
- Local `writing-skills` guidance, 2026-09-27: skills require pressure-scenario validation and must not replace project-specific documentation.

## Open Questions

- Which project name should replace the historical CopyStart brand before public release? Undecided as of 2026-09-30. The repository name stays CopyStart, and the decision is not a delivery gate.
- Path scope is resolved: every file in `.github/instructions/` is path-scoped with `applyTo`. A missing `applyTo` fails `scripts/governance_checks.py`. No separate governance skill is warranted; the check is deterministic.