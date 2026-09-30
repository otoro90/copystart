## Why

CopyStart has no GitHub Actions checks or automated tests, and the historical .NET 6 MVC solution currently fails its build because LibMan cannot resolve a dependency. The new SaaS needs a trustworthy validation and artifact-publication path before its GitOps workload can be activated.

## What Changes

- Add read-only PR checks for OpenSpec, secret exposure, and any build, test, migration, and frontend targets that actually exist at each delivery stage.
- Make the legacy build failure visible without representing the unsupported prototype as the deployable SaaS or upgrading its packages indiscriminately.
- Publish separate API, public-site, and operations `linux/arm64` images to GHCR by immutable digest only after their .NET 10 and Angular 22 targets and checks exist.
- Record provenance, image security results, and SBOM/license evidence against the published digest.
- Promote verified digests through CopyStart-owned development GitOps overlays with a bounded Git-only update; Argo CD owns workload reconciliation.
- Define manual, approval-gated promotion of the same verified digests to production when the later deployment change has approved production activation.

## Capabilities

### New Capabilities
- `saas-ci-and-image-publication`: Validates change artifacts and runnable targets, publishes verifiable ARM64 images, and controls digest-only GitOps promotion without cluster access from CI.

### Modified Capabilities

None.

## Impact

Changes CopyStart-owned GitHub workflows, build tooling, image publication, and Git-only digest updates to CopyStart-owned overlays. PR checks can start once `remediate-legacy-secrets-and-inventory` removes live prototype configuration values and `establish-project-governance` defines verification gates; publishing additionally requires the new backend and frontend deliverables. The CopyStart `provision-saas-platform-prerequisites` change defines application runtime expectations only. The mini-cluster changes own shared prerequisite provisioning and Argo CD Application registration; `deploy-saas-platform-to-mini-cluster` owns workload manifests, ingress/readiness integration, and protected cutover. CI never receives cluster credentials or applies resources; development digest promotion is a Git change only, not a cluster mutation.