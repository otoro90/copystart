# Skill Candidates

A skill is added only when a baseline scenario shows an agent fails without it.

| Candidate | Baseline date | Baseline result | Decision |
| --- | --- | --- | --- |
| Vertical template boundary (odometer rule as template JSON) | 2026-09-30 | Agent chose a typed generic meter capability with declarative template options | Not needed; covered by `templates.instructions.md` |
| Tenant isolation (header tenant, return all when missing) | 2026-09-30 | Agent refused, used token claims, failed closed, separated admin endpoint | Not needed; covered by `multitenancy.instructions.md` |

Revisit when a real implementation session violates one of these rules. Record the failing prompt and response before writing the skill.
