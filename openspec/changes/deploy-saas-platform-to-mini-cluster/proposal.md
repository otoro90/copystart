## Why

The modernized SaaS needs reproducible workload manifests and a controlled development rollout that respects the mini-cluster's Argo CD and Day-1 ownership model. Application image supply-chain automation and platform prerequisite provisioning have separate owners and must not be duplicated here.

## What Changes

- Add CopyStart-owned workload base, development overlay, and disabled production overlay; overlays reference immutable digests published and promoted by `establish-github-ci-and-image-publishing`.
- Define workload services, migration job, probes, resources, and consumption of the runtime configuration contract from CopyStart's `provision-saas-platform-prerequisites`.
- Integrate with mini-cluster-owned Argo CD Applications, Traefik/Cloudflare ingress, ordered readiness, monitoring, backups, and rollback documentation.
- Keep production activation as a separately approved cutover.
- **BREAKING** Remove image build, test/security scanning, SBOM, publication, and digest-promotion ownership from this deployment change; those belong exclusively to `establish-github-ci-and-image-publishing`.

## Capabilities

### New Capabilities
- `saas-runtime-operations`: Provides reproducible GitOps deployment, secrets, data services, ingress, observability, backup, readiness, and rollback for the SaaS runtime.

### Modified Capabilities

None.

## Impact

Touches CopyStart-owned workload manifests/overlays and mini-cluster-owned Argo CD Application registration, ingress integration, and ordered readiness documentation. Depends on the CopyStart runtime contract, mini-cluster's `provision-copystart-platform-prerequisites` and `harden-seaweedfs-s3-security` changes, `establish-github-ci-and-image-publishing`, and all runtime-facing changes. CI has no cluster credentials and this change has no image build/publish workflow. Production activation remains a protected post-bootstrap cutover.