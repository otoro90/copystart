## ADDED Requirements

### Requirement: Pull requests validate without production privileges
The repository MUST run read-only checks for relevant OpenSpec changes, secret leaks, and available build/test targets on pull requests, without cluster credentials, registry write access, or deployment permissions.

#### Scenario: Untrusted pull request changes application code
- **WHEN** a contributor opens a pull request against the protected branch
- **THEN** validation runs on an isolated runner with a read-only token and cannot publish images, modify overlays, or reach protected cluster resources

#### Scenario: A changed OpenSpec artifact is invalid
- **WHEN** a pull request contains an invalid OpenSpec change
- **THEN** the required CI status fails before merge with the affected change name

### Requirement: Validation follows the actual target lifecycle
CI MUST distinguish the legacy MVC prototype from deployable SaaS targets and MUST make missing required targets or failing checks explicit rather than reporting a false green build.

#### Scenario: The legacy LibMan build fails before replacement
- **WHEN** only the historical MVC solution exists and its asset restore fails
- **THEN** CI reports the failure or an explicitly documented legacy diagnostic status, and does not publish it as a SaaS image

#### Scenario: A new project is added
- **WHEN** the .NET 10 API or an Angular application is introduced
- **THEN** the corresponding build, test, lint, and security gates become required before that component can publish

### Requirement: Verified ARM64 images have immutable provenance
The release workflow SHALL build each available deployable API, public-site, and operations component for `linux/arm64`, publish to GHCR only after its gates pass, and bind digest, source commit, security result, and SBOM/license evidence to that image.

#### Scenario: An image fails its security gate
- **WHEN** scanning reports a blocking vulnerability or the image is not an ARM64 artifact
- **THEN** no development or production overlay is promoted to that digest

### Requirement: Promotion changes Git, not the cluster
Development promotion MUST update only the intended CopyStart-owned overlay image digest after verifying it matches the published image. Production promotion MUST require explicit authorization and reuse the identical previously verified digest.

#### Scenario: Two components publish concurrently
- **WHEN** API and frontend builds complete near the same time
- **THEN** promotion serializes or safely retries overlay commits without losing either component's digest

#### Scenario: Production promotion is requested without readiness approval
- **WHEN** production activation gates or protected approval are missing
- **THEN** production overlay and Argo CD synchronization remain unchanged

### Requirement: CI credentials and runners have least privilege
Workflow tokens MUST be job-scoped, third-party actions MUST be pinned to reviewed immutable revisions, and untrusted pull-request code MUST NOT execute on a runner with access to homelab credentials or internal services.

#### Scenario: A pull request tries to read a protected secret
- **WHEN** the PR validation job executes untrusted code
- **THEN** no OpenBao, Kubernetes, GHCR write, or deployment credential is present in its environment