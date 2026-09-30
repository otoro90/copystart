# Validation Evidence

Recorded: 2026-09-30, after tasks 3.1–3.3 were implemented.

## Research

These checks are local and mechanical. Target versions are read from `CONTEXT.md` under `## Target Baseline`. No library, framework, or infrastructure behavior was selected, so no Context7 or vendor-document lookup was required.

## `python3 scripts/governance_checks_test.py`

```text
Ran 8 tests in 0.021s
OK
```

## `python3 scripts/governance_checks.py`

```text
Governance checks passed
```

Passed checks:

- Relative links in `AGENTS.md`, `CONTEXT.md`, `SYSTEM.md`, `docs/`, and `.github/` resolve inside the repository. External URLs are not fetched.
- Authority documents do not claim a target `.NET`, EF Core, Npgsql, Wolverine, or Angular major version other than the `CONTEXT.md` baseline. Legacy, prototype, unsupported, and explicitly stale mentions remain allowed.
- `SYSTEM.md` still states the Definition of Done gates, and every active change requires strict OpenSpec validation.
- Instruction files have `name`, `description`, and `applyTo`. The OpenSpec reviewer agent has `name` and `description`.

`storage.instructions.md` and `templates.instructions.md` were missing `applyTo`. Both now have path globs, matching the other path-scoped instructions.

## `openspec doctor --json`

```json
{
  "root": {
    "path": "/Volumes/MAC/Repo/Personal/copystart",
    "source": "nearest",
    "healthy": true,
    "status": []
  },
  "store": null,
  "references": [],
  "status": []
}
```

## `openspec validate establish-project-governance --strict`

```text
Change 'establish-project-governance' is valid
```

## Archive sync (2026-09-30)

The delta spec was copied to `openspec/specs/engineering-governance/spec.md` before archive. Living docs record two lessons only:

- Missing `applyTo` on instruction files is enforced by `scripts/governance_checks.py`. No governance skill was added.
- `CONTEXT.md` Target Baseline remains the runtime-version source. `llms.txt` repeats it and is included in the drift check. The public brand question stays undecided and is not a delivery gate.
