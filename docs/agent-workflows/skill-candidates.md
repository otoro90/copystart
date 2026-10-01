# Skill Candidates

A skill is added only when a baseline scenario shows an agent fails without it.

| Candidate | Baseline date | Baseline result | Decision |
| --- | --- | --- | --- |
| Vertical template boundary (odometer rule as template JSON) | 2026-09-30 | Agent chose a typed generic meter capability with declarative template options | Not needed; covered by `templates.instructions.md` |
| Tenant isolation (header tenant, return all when missing) | 2026-09-30 | Agent refused, used token claims, failed closed, separated admin endpoint | Not needed; covered by `multitenancy.instructions.md` |
| Legacy secret and source inventory | 2026-09-30 | Distinguish historical exposure from active/reused credentials; require owner approval before source access; inventory only explicitly selected files and never print values. OpenSpec gates and the security guide were sufficient. | No new skill; this was a one-time remediation and no recurring skill-specific failure was demonstrated |
| Governance link, version, and validation checks | 2026-09-30 | Verification found `storage` and `templates` instructions missing `applyTo`. The failure is deterministic. | No skill. `scripts/governance_checks.py` enforces links, target versions, strict-validation tasks, and instruction front matter. A skill would duplicate it. |
| Backend foundation facts (mixed legacy states, GovCore patches treated as pins, closed import question reopened) | 2026-09-30 | An apply session stalled before creating `src/` and would have modeled `En ejecucion` as a request state. | No new skill. `CONTEXT.md`, `backend.instructions.md`, `docs/architecture/backend-foundation.md`, and the CopyStart root note in `openspec-apply-change` hold the facts. `.cursor/skills` points at `.github/skills`. |

Revisit when a real implementation session violates one of these rules. Record the failing prompt and response before writing the skill.
