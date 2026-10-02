## Context

The application needs stable PostgreSQL, object-storage, and OIDC expectations for development and production. The mini-cluster is the platform owner: its `provision-copystart-platform-prerequisites` change provisions app-specific resources and its `harden-seaweedfs-s3-security` change secures the shared gateway. This CopyStart change must define consumption, not reproduce that infrastructure work.

## Goals / Non-Goals

**Goals:**
- Define how the application receives database, private object-storage, and OIDC settings and secret references.
- Require fail-closed configuration and tenant-isolation behavior with dependency-specific readiness.
- Keep development and production contracts distinct and production consumption disabled until approved.

**Non-Goals:**
- Create PostgreSQL roles/databases, SeaweedFS buckets/identities, OpenBao/ESO resources, ZITADEL resources, or Argo CD Applications.
- Modify `scripts/bootstrap-full-cluster.sh`, shared manifests, cluster Terraform, backup schedules, or shared services.
- Deploy application workloads or activate production credentials.

## Decisions

1. CopyStart owns application configuration semantics and validation; mini-cluster owns the Secret/ExternalSecret values, infrastructure resources, and bootstrap implementation.
2. Credentials are supplied at runtime through protected references and never stored in Git, image layers, logs, or API payloads. Non-secret endpoint/issuer/client metadata may use ordinary configuration.
3. The runtime contract covers PostgreSQL, OIDC issuer/client/audience and constrained onboarding credentials when required, plus private S3 endpoint/bucket/region and tenant-scoped authorization. Concrete key names are settled alongside the target host/deployment implementation.
4. Missing or conflicting required settings fail startup or readiness closed; no anonymous storage, development fallback, or cross-tenant object access is allowed.
5. Development integration depends on the mini-cluster provision change and SeaweedFS hardening. Their ownership maps to Day-1 Stage 4 (database and secret references), Stage 5 (storage and shared identity), and Stage 6 (contract readiness). This application contract itself is Day-0 and adds no bootstrap stage.
6. Mini-cluster owns idempotent provisioning, backup/restore, and infrastructure rollback. Application rollback uses the prior verified image/config revision and never deletes retained database or bucket data.
7. Production values remain unpopulated and production workloads remain disabled until the separately approved activation gate.

## Risks / Trade-offs

- [Application expectations drift from injected resources] -> Validate configuration names and readiness against the mini-cluster contract before implementation and deployment.
- [Secret values leak through diagnostics] -> Test redaction and ensure diagnostics expose only key names and dependency state.
- [A service is reachable but authorization is wrong] -> Include tenant-isolation and authenticated storage/OIDC integration tests, not only port checks.

## Migration Plan

1. Agree the database, OIDC, and object-storage configuration contract with the mini-cluster owners.
2. Implement typed configuration validation and fail-closed readiness in the target backend.
3. Add tenant-isolation and authenticated integration checks using development prerequisites.
4. Keep production configuration empty and deployment disabled until an explicit activation approval.

Rollback reverts the application configuration or image to the last verified development revision. It does not mutate platform resources, rotate cluster secrets, or delete database/bucket data. The mini-cluster change documents infrastructure rollback.