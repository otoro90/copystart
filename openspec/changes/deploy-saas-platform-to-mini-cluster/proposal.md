## Why

The modernized SaaS needs a reproducible ARM64 deployment integrated with the existing K3s, Argo CD, PostgreSQL, OpenBao/ESO, SeaweedFS, Traefik, ZITADEL, monitoring, and backup contracts. Ad hoc deployment would violate the mini-cluster Day-1 recovery model.

## What Changes

- Add ARM64-compatible API and frontend images, CI validation, immutable image promotion, and GitOps manifests.
- Route wildcard tenant subdomains through Traefik; Cloudflare Tunnel remains a bridge to Traefik rather than targeting workloads directly.
- Add migrations, liveness/readiness probes, resource limits, logs, metrics, alerts, backups, restore checks, and rollback procedures.
- Consume prerequisites provisioned by `provision-saas-platform-prerequisites` and keep production activation as a separately approved cutover.

## Capabilities

### New Capabilities
- `saas-runtime-operations`: Provides reproducible GitOps deployment, secrets, data services, ingress, observability, backup, readiness, and rollback for the SaaS runtime.

### Modified Capabilities

None.

## Impact

Touches the CopyStart repository for deployable artifacts and overlays and the mini-cluster repository for the Argo CD Application, ingress integration, and readiness documentation. Depends on `provision-saas-platform-prerequisites` and all runtime-facing changes. Production activation remains a protected post-bootstrap cutover.