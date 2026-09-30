## Why

CopyStart has useful domain knowledge but no project context, architecture contract, documentation hierarchy, or project-specific agent guidance. Establishing these controls first prevents the modernization from copying stale GovCore documentation or introducing inconsistent conventions across later changes.

## What Changes

- Establish `CONTEXT.md`, `SYSTEM.md`, `AGENTS.md`, and an architecture overview as distinct sources of truth.
- Define documentation ownership, decision records, validation commands, and Definition of Done.
- Define project-specific instructions and narrowly triggered skills for backend, frontend, tenancy, storage, and deployment work.
- Record GovCore as a pattern source, not a runtime dependency or code-copy target.
- Require versioned primary-source research for material architecture decisions.

## Capabilities

### New Capabilities
- `engineering-governance`: Defines project context, documentation authority, architecture decisions, agent guidance, and validation expectations.

### Modified Capabilities

None.

## Impact

Adds repository-level documentation and agent customization under the CopyStart repository. It does not change application behavior, infrastructure, or production data. This change is a prerequisite for all other modernization changes.