## 1. Prerequisites and Baseline

- [x] 1.1 Verify the legacy-secret remediation state, current `master` branch policy, GitHub repository visibility, and availability of required environment protections
- [x] 1.2 Record the legacy .NET 6/LibMan build failure as a non-deployable diagnostic and identify the new API and Angular project/lockfile paths when created
- [x] 1.3 Agree the first release's vulnerability threshold, license policy, and GHCR visibility without granting PR jobs cluster credentials

## 2. Read-Only Pull Request CI

- [x] 2.1 Add PR workflow checks for changed OpenSpec artifacts and repository secret scanning with no sensitive findings printed
- [x] 2.2 Prove a synthetic leaked secret and an invalid OpenSpec change both fail the workflow without publishing an image
- [x] 2.3 Pin reviewed actions by full commit SHA and restrict PR job tokens to read-only; verify no homelab or package-write credentials reach untrusted code
- [x] 2.4 Add explicit legacy diagnostic output; fail closed if a declared new target is missing rather than treating the unsupported prototype as the SaaS

## 3. Target-Specific Verification

- [ ] 3.1 After the .NET 10 backend exists, require restore, formatting, build, unit/integration tests, tenant-isolation tests, and EF migration checks against isolated PostgreSQL
- [ ] 3.2 After each Angular 22 app exists, require lockfile-based install, lint, type/build, accessibility, and component tests independently for public-site and operations
- [ ] 3.3 Prove a failing test or missing expected target blocks publication while unrelated documentation-only changes do not trigger image builds

## 4. Verified Image Publication

- [ ] 4.1 After target Dockerfiles exist, build API, public-site, and operations images for `linux/arm64` on isolated runners
- [ ] 4.2 Check the published GHCR image platform, immutable digest, source provenance, SBOM/license evidence, and blocking vulnerability policy for each image
- [ ] 4.3 Confirm GHCR package read permissions and cluster-side pull identity are provisioned without exposing them to PR jobs

## 5. Git-Only Promotion and Validation

- [ ] 5.1 Enable development promotion only after `gitops/overlays/dev` exists and cluster prerequisites are ready; update only verified component digests and validate Kustomize rendering
- [ ] 5.2 Test concurrent component promotions, non-fast-forward retry, and prevention of rebuild loops from digest-only commits
- [x] 5.3 Keep production promotion disabled until approved environment protection, identical-digest reuse, and the deployment change's protected cutover gates are verified
- [x] 5.4 Update the CopyStart delivery guide with CI evidence, Git-only promotion readiness, and rollback; run `openspec validate establish-github-ci-and-image-publishing --strict`