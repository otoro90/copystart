# CI and Image Publication

Verified: 2026-09-30.

## Current Gate

Read-only checks run on pull requests and on pushes to `master`. They validate changed OpenSpec artifacts, scan the working tree for secrets, verify the .NET 10 backend foundation via `scripts/ci/check-backend.sh` using pinned `actions/setup-dotnet` v6.0.0 (`a98b56852c35b8e3190ac28c8c2271da59106c68`), and fail if an undeclared deployable target appears. They do not publish images or change the cluster.

Image publication and Git promotion stay disabled. The legacy .NET 6 MVC prototype is a diagnostic only. The .NET 10 modular monolith is declared in `ci/deployable-targets.json` (`CopyStart.sln` and the six projects under `src/`) with `publishable: false` (no SaaS Dockerfile exists yet), and `gitops/overlays/dev` does not exist. The LibMan `LIB002` failure stays a legacy diagnostic; the target classification is retire-with-Razor, recorded in [the backend foundation analysis](../architecture/backend-foundation.md) and [the dependency inventory](../architecture/dependency-inventory.md). Legacy import is not applicable because no authoritative database snapshot exists.

## Repository Baseline

| Check | Result |
| --- | --- |
| Legacy secret remediation | Archived as `2026-09-30-remediate-legacy-secrets-and-inventory`. Current `appsettings` files have no database password. Compose reads `COPYSTART_DB_CONNECTION` and `COPYSTART_DB_PASSWORD` from an ignored env file. |
| Visibility | GitHub repository `otoro90/copystart` is public. |
| Default branch | `master`, not protected, no rulesets. Direct commits to `master` remain the repository policy, so the same read-only checks also run on push. |
| Environments | Only the `copilot` environment exists, and it has no protection rules. A production environment is not configured. Required reviewers are available for this public repository and are not turned on. |
| Cluster credentials | Pull-request jobs set `contents: read` on `ubuntu-latest`. They do not receive a kubeconfig, OpenBao token, or package-write token. |

Historical credential-bearing commits remain in Git. The scanner reads the current working tree and does not walk that history. Three ASP.NET Identity password hashes in the retired prototype are ignored by path, rule, and line. A hash that moves fails closed.

## Publication Policy

These rules apply when a later slice adds Dockerfiles and a publish job. Nothing in the current workflow can publish.

| Decision | Rule |
| --- | --- |
| Vulnerability threshold | Block publication on critical and high findings in the image. Record medium and lower findings without blocking the first pilot. |
| Licenses | Allow MIT, Apache-2.0, BSD-2-Clause, BSD-3-Clause, ISC, and MPL-2.0. Block GPL, AGPL, SSPL, and unclassified licenses on distributed runtime dependencies. |
| Registry | Publish private GHCR packages by immutable digest. The public source repository does not make those packages public. |
| Pull identity | The cluster pull identity belongs to mini-cluster `provision-copystart-platform-prerequisites`. It is not created here and is not available to pull-request jobs. |
| Runners | GitHub-hosted `ubuntu-latest` until an isolated ARM64 runner has its own reviewed boundary. |
| Actions | Pin third-party actions to a full commit SHA. |

## Legacy Diagnostic

`Application/CopyStart.sln` targets the legacy .NET 6 MVC prototype. `Application/CopyStart/libman.json` still restores `datatables.net@1.12.1`, which fails LibMan with `LIB002`. `Application/Dockerfile` is the matching .NET 6 debug container. CI prints `LEGACY_DIAGNOSTIC` and does not build or publish it.

A new `.csproj`, solution, `angular.json`, lockfile, or Dockerfile outside that legacy set fails until `ci/deployable-targets.json` declares it. A declared path that is missing fails closed.

## Promotion and Rollback

Development promotion is not enabled. It waits for `gitops/overlays/dev`, the target images, and the mini-cluster prerequisite checks. Production promotion is not enabled. The public repository can use required reviewers, and those reviewers are not configured. Production also waits for the deployment change's cutover gates and for reuse of an already verified digest.

Rollback of this slice is to disable `.github/workflows/pr-checks.yml` or revert the commit. No digest has been published, so there is no overlay to roll back. Later digest rollback is a Git revert of the overlay; Argo CD keeps cluster ownership.

## Research

| Claim | Source | Result |
| --- | --- | --- |
| Pin actions by full commit SHA and keep `GITHUB_TOKEN` least privilege | GitHub Actions docs, accessed 2026-09-30, via Context7 `/websites/github_en_actions` | `actions/checkout` v7.0.1 is `3d3c42e5aac5ba805825da76410c181273ba90b1`. `actions/setup-node` v7.0.0 is `820762786026740c76f36085b0efc47a31fe5020`. |
| Environment reviewers exist for public repositories on current plans | GitHub Actions environment docs, accessed 2026-09-30 | Available, not configured. This change does not depend on them. |
| Working-tree secret scan | gitleaks 8.30.1, local binary; GitHub asset digest `551f6fc83ea457d62a0d98237cbad105af8d557003051f41f3e7ca7b3f2470eb` | `gitleaks dir` does not scan Git history. `--redact` is the log mode. |
| OpenSpec strict validation | `@fission-ai/openspec` 1.6.0, the CLI already used in this repository | An empty change fails strict validation with the change name. |

Tramites workflows separate pull-request checks from GHCR publication. Their actions are tag-pinned, so this repository does not copy those workflow files.

Residual risk: `master` is unprotected, so GitHub will not require these checks until branch protection is a separate decision. The push workflow still runs them. Governance tasks 3.1–3.3 in `establish-project-governance` remain open; this workflow validates changed OpenSpec artifacts and does not close that change.
