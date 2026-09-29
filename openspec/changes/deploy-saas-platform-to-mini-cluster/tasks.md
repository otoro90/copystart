## 1. Build and Release

- [ ] 1.1 Verify all development prerequisites and runtime contracts are ready
- [ ] 1.2 Add reproducible linux/arm64 builds for API, public SSR, and operations images
- [ ] 1.3 Add unit/integration/E2E gates, vulnerability scanning, SBOM, license checks, and immutable digest publication
- [ ] 1.4 Add CopyStart-owned Kustomize base, development overlay, and disabled production overlay

## 2. GitOps and Networking

- [ ] 2.1 Add mini-cluster-owned development and disabled production Argo CD Applications under the root app
- [ ] 2.2 Add service accounts, ESO consumption, migration PreSync Job, services, probes, and resource limits
- [ ] 2.3 Validate the selected tenant domain's wildcard DNS, certificate depth, Cloudflare Tunnel route, Traefik host rules, and ZITADEL redirect URIs
- [ ] 2.4 Prove Cloudflare targets only Traefik and preserves the original host for tenant resolution

## 3. Operations and Day-1

- [ ] 3.1 Integrate workload rollout into Day-1 Stage 3 and application checks into Stage 6 without duplicating prerequisite provisioning
- [ ] 3.2 Add structured logs, metrics, dashboards, alerts, and dependency-specific readiness checks
- [ ] 3.3 Verify database and object backups, isolated restore, and workload rollback on ARM64
- [ ] 3.4 Update CopyStart and mini-cluster context, deployment, incident, and recovery documentation

## 4. Protected Activation

- [ ] 4.1 Run tenant isolation, OIDC, S3, migration, accessibility, performance, and representative journey acceptance suites in development
- [ ] 4.2 Rehearse image, manifest, schema, and data rollback without pruning retained state
- [ ] 4.3 Keep production unsynchronized until a named pilot, window, owner, backup, and rollback approval are recorded
- [ ] 4.4 Run strict OpenSpec validation in both repositories and the canonical non-mutating cluster readiness profile