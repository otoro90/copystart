## Context

The mini-cluster uses K3s on ARM64, Traefik as the only ingress, Argo CD as workload owner, OpenBao/ESO for secrets, and a canonical Day-1 facade. Tramites demonstrates the repository split where the application owns overlays and mini-cluster owns the Argo CD Application. Cloudflare Tunnel configuration currently documents fixed public hostnames, so wildcard tenant routing and certificate scope require explicit validation.

## Goals / Non-Goals

**Goals:**
- Deploy development workloads reproducibly with observable readiness and rollback.
- Preserve GitOps ownership and host-based tenant resolution.
- Define, but do not automatically execute, production activation.

**Non-Goals:**
- Reprovision shared database, identity, secrets, or storage prerequisites.
- Route Cloudflare directly to application services.
- Claim high availability on the current single active node.

## Decisions

1. CopyStart owns Kustomize base plus `dev` and disabled `prod` overlays. Mini-cluster owns `copy-start-dev` and disabled `copy-start-prod` Argo CD Applications under the root app.
2. CI owns image builds, tests, security/license scanning, SBOM/provenance, GHCR publication, and digest-only updates to CopyStart overlays. This change owns no such workflows or image mutation.
3. Run schema migrations as a bounded PreSync Job with advisory locking, deterministic completion, and generated idempotent SQL retained with application migrations.
4. CopyStart owns workload manifests and dev/disabled-prod overlays. Mini-cluster owns the root-app Argo CD Application registration, shared namespaces/policies as applicable, ingress integration, and ordered readiness contract. Workloads consume protected runtime settings provisioned by the mini-cluster change; CopyStart does not create platform secrets or resources.
5. Route Cloudflare only to Traefik. Adopt a dedicated tenant-domain suffix only after validating wildcard DNS, tunnel hostname routing, ZITADEL redirect URIs, and a certificate covering that exact depth. Fixed administrative/API hosts remain separate.
6. Stage mapping: Day-1 Stage 2 registers the Argo CD Applications from the mini-cluster root app; Stage 3 rolls out development workloads after database, identity, and storage prerequisites; Stage 6 checks migrations, endpoints, tenant host preservation, auth discovery, storage, metrics, and representative journeys. CopyStart defines the workload/readiness contract; mini-cluster implements bootstrap sequencing and cluster-side checks.
7. Production has no automated sync until a named pilot cutover approves database/file migration, backup and restore, security tests, performance budgets, and rollback.

## Risks / Trade-offs

- [Nested wildcard hostname is not covered by existing TLS] -> Provision and verify the exact certificate/DNS scope before exposing tenant domains.
- [Single-node outage stops all services] -> State pilot-grade availability honestly and rely on tested restore rather than false HA claims.
- [SSR increases runtime footprint] -> Measure and set resource limits; allow SSG/caching for stable public pages.
- [Cross-repository deployment drifts] -> Validate CopyStart overlay rendering in CI, validate mini-cluster Argo registration in its repository, and document coordinated release order.

## Migration Plan

1. Verify runtime-contract and mini-cluster prerequisite/hardening changes are approved and development prerequisites are ready.
2. Add CopyStart workload base and development/disabled-production overlays with digest placeholders; CI later supplies verified digests.
3. Register the Argo CD Applications in the mini-cluster root app and integrate Stage 2/3/6 ownership.
4. Verify ingress, tenant resolution, OIDC, S3, metrics, logs, backup, restore, and workload rollback in development.
5. Keep production unsynchronized and document the protected pilot cutover.

Rollback reverts image digests or Git manifests and lets Argo CD reconcile. Database rollback uses the approved migration/data restore procedure; retained PVCs, buckets, and backups are never pruned with the workload.

## Research Evidence

- Mini-cluster `CONTEXT.md`, `SYSTEM.md`, and `bootstrap-full-cluster.sh`, inspected 2026-09-27: Traefik-only ingress, Argo CD ownership, ARM64 constraints, six-stage Day-1 contract, and protected production cutover expectations.
- Local Tramites `gitops` and mini-cluster Argo CD Applications, inspected 2026-09-27: application repository owns overlays; root-app repository owns Applications; development auto-sync and production disabled are established patterns.
- Local Cloudflare Tunnel manifest, inspected 2026-09-27: public routes target Traefik and are currently managed as documented fixed hostnames.
- Cloudflare platform guidance, accessed 2026-09-27: current product/network behavior must be validated from official documentation before configuring wildcard routes.

## Open Questions

- What final product and tenant base domain will be purchased and delegated?
- Will public SSR run in the same pod class as static operations assets or require separate scaling?
- What production RPO/RTO is acceptable for paid pilots?