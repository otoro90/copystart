## ADDED Requirements

### Requirement: Workloads are GitOps-owned and ARM64-compatible
CopyStart API, public site, and operations application MUST be built as immutable ARM64-compatible images and deployed only through reviewed GitOps manifests reconciled by Argo CD.

#### Scenario: A development image is promoted
- **WHEN** CI publishes verified image digests and updates the development overlay
- **THEN** Argo CD reconciles exactly those digests without an imperative production deployment

### Requirement: Application and cluster repositories have separate ownership
The CopyStart repository SHALL own workload bases and environment overlays, while mini-cluster SHALL own the Argo CD Applications and shared platform integration.

#### Scenario: CopyStart overlay changes
- **WHEN** the application repository merges a valid development overlay
- **THEN** the mini-cluster Application observes and reconciles it without duplicating workload manifests locally

### Requirement: Tenant ingress is explicit and secure
Tenant hostnames MUST terminate with valid TLS, route through Cloudflare Tunnel to Traefik only, preserve the host used for tenant resolution, and avoid collisions with other platform applications.

#### Scenario: Tenant subdomain reaches the platform
- **WHEN** a request arrives for an approved tenant hostname
- **THEN** Cloudflare forwards to Traefik, Traefik selects the intended public or operations service, and the original host remains available for verified tenant lookup

### Requirement: Runtime readiness is observable
Deployments SHALL define liveness, readiness, startup behavior, resource requests/limits, structured logs, metrics, dashboards, alerts, and rollout checks for API, frontend, database migration, identity, and storage dependencies.

#### Scenario: SeaweedFS is unavailable
- **WHEN** the API cannot satisfy its required storage readiness contract
- **THEN** readiness fails, traffic is withheld as designed, and monitoring identifies the dependency without leaking credentials

### Requirement: Production activation is protected
Production MUST remain unsynchronized or otherwise inactive until backup, restore, migration, security, performance, and rollback evidence is approved for a named pilot.

#### Scenario: Development deployment succeeds
- **WHEN** all development readiness checks pass
- **THEN** production remains inactive until a separate approved cutover is executed