# CopyStart Agent Rules

CopyStart is being rebuilt from a single-company ASP.NET Core 6 MVC prototype into a multi-tenant SaaS for small repair and technical-service businesses.

## Read First

- [CONTEXT.md](CONTEXT.md): current verified state and technology baseline.
- [SYSTEM.md](SYSTEM.md): delivery rules and Definition of Done.
- [docs/README.md](docs/README.md): architecture, decisions, and workflow guides.
- [docs/architecture/backend-foundation.md](docs/architecture/backend-foundation.md): verified backend analysis. The .NET 10 solution is not created yet.
- `openspec/changes/`: planned behavior. OpenSpec is the source of truth for changes. Run the CLI from this repository root; the mini-cluster checkout is a different OpenSpec root.

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

## Git Workflow

- For routine, low-risk work, make focused commits directly on the repository's configured primary branch. The current remote has only `master`; CI and delivery documentation also target `master`.
- The owner's desired `main`-only workflow requires a separate, coordinated migration of the GitHub default branch, workflow triggers, and documentation. Do not create or push a parallel `main` branch, rename the remote default, or migrate while the worktree contains uncommitted implementation work. Until that migration is completed, use `master` as the actual primary branch.
- Use a Pull Request with CI and a reviewed plan for production infrastructure, security/identity, database migration, destructive data, or other high-impact changes when the owning workflow supports that gate.
- Check `git status` before syncing. Run `git pull --rebase origin master` only when the worktree is clean; preserve dirty changes and coordinate with the collaborator to avoid overlapping work.
- Never force-push. If the primary branch is protected, diverged, or cannot be updated with a fast-forward, stop and report the blocker.
