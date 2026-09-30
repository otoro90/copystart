## 1. Source-of-Truth Documents

- [x] 1.1 Create concise `CONTEXT.md`, `SYSTEM.md`, and `AGENTS.md` files with distinct ownership
- [x] 1.2 Create architecture, development, testing, security, and delivery documentation indexes
- [x] 1.3 Record GovCore extraction boundaries and current verified technology versions
- [x] 1.4 Populate `openspec/config.yaml` with stable project context and artifact rules

## 2. Agent Guidance

- [x] 2.1 Add path-scoped instructions for backend, frontend, multitenancy, storage, and GitOps work
- [x] 2.2 Identify recurring judgment-heavy workflows that justify skills rather than ordinary documentation
- [x] 2.3 Run baseline pressure scenarios before creating each approved skill
- [x] 2.4 Create minimal skills, rerun pressure scenarios, and verify discovery without duplicating canonical guidance (baselines passed; no skill justified yet, recorded in `docs/agent-workflows/skill-candidates.md`)

## 3. Governance Verification

- [x] 3.1 Add documentation link, version-drift, and required-validation checks
- [x] 3.2 Verify all links and customization files and run `openspec doctor --json`
- [x] 3.3 Run `openspec validate establish-project-governance --strict` and record fresh evidence