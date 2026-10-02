## Context

CopyStart is being rebuilt from a retired .NET 6 MVC prototype into a multi-tenant SaaS for repair and technical-service businesses. The delivery plan spans 13 sequential changes in `docs/delivery/roadmap.md`, distributed across two repositories: `copystart` (application monolith and frontend) and `mini-cluster` (shared K3s ARM64 cluster hosting Traefik, SeaweedFS S3, PostgreSQL, OpenBao/ESO, and ZITADEL).

Previous agent executions suffered repeated stall cycles. The root cause was an uncoordinated dependency loop:
1. `copystart` change 5 (`add-multitenancy-and-zitadel-identity`) task 2.1 requested real ZITADEL credentials from platform prerequisites.
2. `mini-cluster` change 8 (`provision-copystart-platform-prerequisites`) depended on change 7 (`harden-seaweedfs-s3-security`).
3. `mini-cluster` change 7 hesitated to proceed due to fear of breaking `GovCo.Tramites` DEV storage or needing live unsealed OpenBao sessions.

To eliminate stalls and allow autonomous progression, this design establishes a decoupled 4-phase execution runbook, strict blast-radius isolation rules, and in-memory test conventions.

## Goals / Non-Goals

**Goals:**
- Provide a single, unambiguous execution runbook linking all 13 roadmap gates across `copystart` and `mini-cluster`.
- Decouple application code completion from live cluster state using in-memory test fixtures and strongly-typed configuration contracts.
- Guarantee zero blast radius for existing cluster tenants, specifically `GovCo.Tramites` and `Ecclesiae`.
- Unblock Change 5 and Change 6 in `copystart` so they can be implemented, verified with `dotnet test`, and archived.
- Define explicit handoff checkpoints between repositories.

**Non-Goals:**
- Merging the two repositories into a monorepo.
- Provisioning production infrastructure or importing legacy prototype data.
- Introducing a transactional outbox before external side effects require at-least-once delivery.

## Decisions

### Decision 1: Decoupled In-Memory Verification for Application Slices
- **Choice**: Implement and test OIDC authentication, multitenancy resolution, and database filters in `copystart` using `TestAuthenticationHandler`, mocked claims, and in-memory/isolated database contexts.
- **Rationale**: Application logic must not depend on physical network availability or VPN connections to pass unit and integration CI gates. This allows backend changes (5 and 6) to finish cleanly without blocking on cluster provisioning (8).
- **Alternatives Considered**: Requiring live ZITADEL container via Testcontainers in CI (rejected: unnecessary resource overhead on developer machines and CI runners for basic claim-validation tests).
- **Sources**: Microsoft ASP.NET Core Authentication Testing documentation (accessed 2026-10-01).

### Decision 2: Strict Shared-Infrastructure Blast-Radius Guardrails
- **Choice**: Mini-cluster changes (7 and 8) must enforce explicit resource namespacing:
  - PostgreSQL: Only create `copystart_dev_db` and role `copystart_dev_user`. Grant no permissions on `tramites_*`.
  - SeaweedFS S3: Audit and preserve `tramites-app` bucket and credentials. Remove anonymous read globally. Provision isolated bucket `copystart-dev-files`.
  - ZITADEL: Create isolated Organization `copystart` and Project `CopyStart Platform`. Do not mutate Tramites projects.
  - OpenBao: Mount secrets only under path `secret/data/copystart/dev/*`.
- **Rationale**: `GovCo.Tramites` is an active system in development and staging on the mini-cluster; isolation prevents cross-tenant regressions.
- **Sources**: `mini-cluster/CONTEXT.md` and `Tramites/AGENTS.md` (verified 2026-10-01).

### Decision 3: Four-Phase Sequential Execution Pipeline
- **Choice**: Group the 13 changes into 4 manageable phases executed in strict sequence:
  - Phase 1: CopyStart Backend Core (Changes 5 and 6 in `copystart`).
  - Phase 2: Mini-Cluster Infrastructure Prerequisites (Changes 7 and 8 in `mini-cluster`).
  - Phase 3: CopyStart Multisurface Frontend & Templates (Changes 9 and 10 in `copystart`).
  - Phase 4: Integration, Entitlements & GitOps Deployment (Changes 11, 12, 13 across both).
- **Rationale**: Eliminates context drift, keeps task lists bounded within single agent context windows, and respects OpenSpec gating.
- **Alternatives Considered**: Executing all changes simultaneously (rejected: caused the current blocking loop).

## Infrastructure and Day-1 Integration

- **Day-1 Stage**: Mini-cluster Phase 2 maps to Stage 4 (Database & Secrets) and Stage 5 (Storage & Identity) of `scripts/bootstrap-full-cluster.sh`.
- **Idempotency**: All database, bucket, and ZITADEL provisioning scripts in change 8 must be idempotent; re-running must not overwrite existing passwords or drop databases.
- **Readiness Check**: Validated via `scripts/check-ordered-readiness.sh` and `scripts/bootstrap-full-cluster.sh --profile verify --check-only` without disclosing credentials.
- **Rollback**: Scoped teardown of `copystart-dev` namespace and database role without affecting other cluster tenants.

## Risks / Trade-offs

- **[Risk]** Divergence between in-memory test mocks and real ZITADEL token signatures.
  → **Mitigation**: Change 6 defines strict JWT validation rules matching ZITADEL's RFC 7519 claims; end-to-end rehearsal occurs in Change 13.
- **[Risk]** SeaweedFS hardening breaks existing Tramites DEV file uploads.
  → **Mitigation**: Task 1.5 in Change 7 explicitly inventories and preserves `tramites-app` credentials before disabling anonymous read.
- **[Risk]** Agent amnesia between phases.
  → **Mitigation**: The execution runbook in `docs/delivery/master-execution-runbook.md` provides an explicit status checklist with exact CLI commands for handoff.

## Migration Plan

1. Create this orchestration change and document the runbook.
2. Edit Task 2.1 of Change 5 (`add-multitenancy-and-zitadel-identity`) to reference the decoupled mock strategy.
3. Execute Phase 1: finish and archive Change 5 and Change 6.
4. Execute Phase 2: finish and archive Change 7 and Change 8 in `mini-cluster`.
5. Execute Phase 3: implement Angular frontend and vertical templates (Changes 9 and 10).
6. Execute Phase 4: S3 file migration, subscriptions, and GitOps Argo CD deployment (Changes 11, 12, 13).

## Open Questions

None. Decoupling and isolation rules are aligned with repository baseline and user requirements.
