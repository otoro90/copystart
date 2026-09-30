## 1. Prerequisite Verification

- [ ] 1.1 Verify CopyStart runtime-contract, mini-cluster prerequisite/SeaweedFS-hardening, and CI publication changes are complete enough for workload integration
- [ ] 1.2 Confirm CI owns image builds, tests, scanning, SBOM/provenance, GHCR publication, and digest-only overlay promotion

## 2. Workload Manifests and GitOps Integration

- [ ] 2.1 Add CopyStart-owned Kustomize base, development overlay, and disabled production overlay referencing immutable image digests
- [ ] 2.2 Add workload service accounts, runtime secret references, migration PreSync Job, services, probes, and resource limits without defining shared secret values
- [ ] 2.3 Add mini-cluster-owned development and disabled production Argo CD Applications under the root app
- [ ] 2.4 Validate selected tenant-domain wildcard DNS, certificate depth, Cloudflare Tunnel route, Traefik host rules, and ZITADEL redirect URIs
- [ ] 2.5 Prove Cloudflare targets only Traefik and preserves the original host for tenant resolution

## 3. Runtime Operations and Day-1

- [ ] 3.1 Integrate workload rollout into Day-1 Stage 3 and application checks into Stage 6 without moving the canonical facade or prerequisite creation into CopyStart
- [ ] 3.2 Add structured logs, metrics, dashboards, alerts, and dependency-specific workload readiness checks
- [ ] 3.3 Verify mini-cluster database/object backups and isolated restore contract, plus workload rollback without pruning retained data
- [ ] 3.4 Update CopyStart workload and mini-cluster Argo/ingress/readiness ownership documentation

## 4. Protected Activation

- [ ] 4.1 Run tenant-isolation, OIDC, S3, migration, accessibility, performance, and representative journey acceptance suites against development
- [ ] 4.2 Rehearse image, manifest, schema, and data rollback without pruning retained state
- [ ] 4.3 Keep production unsynchronized until a named pilot, window, owner, backup, and rollback approval are recorded
- [ ] 4.4 Run `openspec validate deploy-saas-platform-to-mini-cluster --strict` and the mini-cluster change validations; verify canonical non-mutating cluster readiness