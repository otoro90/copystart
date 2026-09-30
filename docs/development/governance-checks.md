# Governance Checks

`CONTEXT.md` Target Baseline is the only current runtime-version source. Run this before marking a documentation, instruction, or OpenSpec task change complete:

```bash
python3 scripts/governance_checks.py
python3 scripts/governance_checks_test.py
```

The command checks four things:

| Check | What fails |
| --- | --- |
| Documentation links | A relative link under `AGENTS.md`, `CONTEXT.md`, `SYSTEM.md`, `docs/`, or `.github/` does not resolve to a file or directory in this repository |
| Version drift | `CONTEXT.md` Target Baseline is the only current runtime source. A copy in authority docs, `openspec/config.yaml`, or `llms.txt` states another target `.NET`, EF Core, Npgsql, Wolverine, or Angular major version. Legacy, prototype, unsupported, and explicitly stale mentions are allowed |
| Required validation | `SYSTEM.md` drops a Definition of Done gate, or an active change's `tasks.md` does not require strict OpenSpec validation |
| Customization files | An instruction file lacks `name`, `description`, or `applyTo`, or a custom agent lacks `name` or `description` |

External URLs are not fetched. GitHub Actions wiring belongs to `establish-github-ci-and-image-publishing`.
