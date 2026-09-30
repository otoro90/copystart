## Context

CopyStart currently has no `.github/workflows/` and only an unsupported .NET 6 MVC solution. On 2026-09-30, `dotnet build Application/CopyStart.sln --no-restore` failed with LibMan `LIB002` for `datatables.net@1.12.1`; the NuGet audit also reported vulnerable transitive packages. The target is a .NET 10 API plus two Angular 22 applications, none of which exist yet. Tramites separates read-only PR checks, GHCR image publishing, and Git-only digest promotion; mini-cluster owns the Argo CD Application and protected infrastructure.

## Goals / Non-Goals

**Goals:**
- Establish truthful, least-privilege CI before migrating dependencies or publishing workloads.
- Publish reproducible and verifiable ARM64 artifacts when target components exist.
- Promote by Git-only digest updates without CI access to the homelab.

**Non-Goals:**
- Upgrade or deploy the legacy MVC app as the SaaS.
- Create application workloads, Argo CD Applications, cluster resources, or OpenBao secrets.
- Activate production automatically or claim integration checks pass before dependencies exist.

## Decisions

1. PR jobs use GitHub-hosted isolated runners, `pull_request` (not `pull_request_target`), and `contents: read`. No cluster kubeconfig, runner with homelab access, registry write permission, or secret-bearing environment is available. This is preferred over reusing the persistent mini-cluster runners, because untrusted jobs could access internal services.
2. Stage gates by source availability: OpenSpec/secret checks from the first CI slice; a truthful diagnostic for the legacy build; .NET 10 build/tests/migration drift only after the backend exists; Angular lint/test/build for each app only when its manifest and lockfile exist. Absence of an expected newly introduced target fails closed.
3. PR checks do not require live PostgreSQL or ZITADEL. Integration tests use isolated service containers or vetted fixtures; live environment tests belong to the later deployment change.
4. A protected publish job builds `linux/arm64` API, public SSR, and operations images into GHCR using only the job-scoped `packages: write` token. It verifies architecture and blocking security policy, generates an SBOM and source provenance, and records the immutable digest. Use reviewed commit-SHA-pinned actions, not mutable branches or blindly copied tags from Tramites.
5. Promotion consumes a verified digest, updates only the matching image entry in a CopyStart-owned development Kustomize overlay, validates the render, and pushes a bounded Git change. Serialize component promotions or retry non-fast-forward against the latest branch. The workflow never invokes `kubectl`, Terraform, or Argo CD directly.
6. A production promotion, if eventually authorized, reuses the same digest with no rebuild and requires an explicit approved gate and a disabled-until-approved production Argo CD Application. Verify the repository's GitHub plan supports the intended environment protection before depending on it.
7. Avoid automatic deployment on every historical `master` commit. Enable promotion only once the target project, image, overlays, and prerequisite checks exist. Branch renaming to `main` is outside this change.

## Risks / Trade-offs

- [Existing legacy credentials make full-history secret scanning fail] -> Inventory/rotate and classify them in `remediate-legacy-secrets-and-inventory`; never silently allowlist active credentials. New-change scanning can start only after the tracked values are removed.
- [Buildx/QEMU is slow] -> Measure ARM64 build time; consider native isolated ARM64 runners only after their security boundary is designed and tested.
- [Published image has no deployment contract yet] -> Leave digest unpromoted until the overlay and cluster ownership checks exist.
- [Automated overlay commit retriggers build] -> Exclude digest-only changes from image-build triggers while still validating overlay render.
- [Private GHCR image cannot be pulled] -> A conditional package-read-only image-pull identity belongs to mini-cluster `provision-copystart-platform-prerequisites`; verify it before rollout. CopyStart's same-slug change defines runtime consumption only.
- [Cross-change ownership drifts] -> This change owns PR checks, build/test/security gates, SBOM/provenance, GHCR publication, and Git-only digest promotion. Deployment owns workload manifests, Argo CD integration, ingress, runtime readiness, and production cutover; mini-cluster owns shared prerequisites and Argo CD Application registration.

## Migration Plan

1. Confirm the credential remediation gate and current `master` branch policy; define a build-matrix contract that distinguishes legacy diagnostics from target deliverables.
2. Add read-only PR checks and prove a synthetic secret, invalid spec, and failed build cause failure without protected credentials.
3. Activate new component jobs only after the .NET 10 and Angular projects include tests and lockfiles; document baseline evidence and dependency changes.
4. Once target Dockerfiles exist, publish to GHCR and validate ARM64 digest, scan results, SBOM, and provenance without changing GitOps.
5. Once development overlays and cluster prerequisites exist, enable digest promotion, test concurrent updates and rollback to a previously verified digest.
6. Leave production promotion disabled until separate deployment acceptance and approved protection rules are demonstrated.

Rollback disables publishing/promotion workflows or reverts the overlay digest in Git. Published immutable images remain available for traceability; Argo CD reconciles a reverted overlay only under the later deployment contract.

## Bootstrap Impact

GitHub PR/build/publish jobs are GitHub-side operations outside Day-1 bootstrap. A digest commit is a Git-only release event, not a bootstrap mutation. The mini-cluster owns Stage 2 root Application registration and shared Stage 4/5 prerequisites; deployment integration owns Stage 3 workload rollout and Stage 6 application readiness. The CopyStart `provision-saas-platform-prerequisites` change defines only the application runtime contract. No new bootstrap module or cluster credentials are authorized by this CI change. Readiness for this change is CI status, GHCR digest and attestations, and successful local Kustomize render; rollback reverts a Git digest and leaves cluster state ownership with Argo CD.

## Research Evidence

- CopyStart `Application/CopyStart/CopyStart.csproj` and repository workflow inventory, inspected 2026-09-30: only .NET 6 MVC exists and no CI workflow is present. `dotnet build --no-restore` failed with LibMan `LIB002`.
- Tramites `pr-backend.yml`, `pr-frontend.yml`, `ci-backend.yml`, `ci-frontend.yml`, and `scripts/update-digest.sh`, inspected 2026-09-30: useful separation of PR checks, GHCR publication, and Git-only promotion; action pinning and secret handling require independent review.
- mini-cluster `SYSTEM.md`, `docs/iac-ownership-matrix.md`, and `scripts/bootstrap-full-cluster.sh`, inspected 2026-09-30: Argo CD owns workloads and the canonical Day-1 facade owns protected prerequisites and readiness.
- GitHub official Actions security, token-permissions, image-publication, and self-hosted-runner documentation, accessed 2026-09-30: least privilege, immutable action pinning, GHCR with `GITHUB_TOKEN`, and persistent-runner risk. Docker official Buildx documentation, accessed 2026-09-30: ARM64 cross-build/QEMU tradeoffs.

## Open Questions

- Is CopyStart public or private, and does the account plan support required reviewers on the intended environment?
- Will GHCR images be private, and which cluster-side pull identity will own them?
- Which vulnerability threshold and license policy must block publication for the first pilot?