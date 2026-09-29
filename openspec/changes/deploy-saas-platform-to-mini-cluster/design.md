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
2. Build separate API, Angular public SSR, and Angular operations images for linux/arm64, pin production deployments by digest, and generate SBOM/license evidence.
3. Run schema migrations as a bounded PreSync Job with advisory locking, deterministic completion, and generated idempotent SQL retained with application migrations.
4. Use dedicated namespaces and service accounts. Consume only ESO-generated secrets from `provision-saas-platform-prerequisites`.
5. Route Cloudflare only to Traefik. Adopt a dedicated tenant-domain suffix only after validating wildcard DNS, tunnel hostname routing, ZITADEL redirect URIs, and a certificate covering that exact depth. Fixed administrative/API hosts remain separate.
6. Stage mapping: Stage 2/root app registers Argo Applications; Stage 3 rolls out development workloads after prerequisites; Stage 6 checks migrations, endpoints, tenant host preservation, auth discovery, storage, metrics, and representative journeys.
7. Production has no automated sync until a named pilot cutover approves database/file migration, backup and restore, security tests, performance budgets, and rollback.

## Risks / Trade-offs

- [Nested wildcard hostname is not covered by existing TLS] -> Provision and verify the exact certificate/DNS scope before exposing tenant domains.
- [Single-node outage stops all services] -> State pilot-grade availability honestly and rely on tested restore rather than false HA claims.
- [SSR increases runtime footprint] -> Measure and set resource limits; allow SSG/caching for stable public pages.
- [Cross-repository deployment drifts] -> Contract-test overlays and Argo Applications in CI and document coordinated release order.

## Migration Plan

1. Complete prerequisite readiness and integration contracts.
2. Add ARM64 builds, vulnerability/license checks, and immutable image publication.
3. Add CopyStart workload base and development overlay.
4. Register the development Argo CD Application and integrate ordered readiness.
5. Verify ingress, tenant resolution, OIDC, S3, metrics, logs, backup, restore, and rollback in development.
6. Add a disabled production overlay/Application and document the protected pilot cutover.

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