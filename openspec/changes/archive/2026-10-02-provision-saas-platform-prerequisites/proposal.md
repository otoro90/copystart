## Why

CopyStart needs a stable application-side contract for database connectivity, tenant-scoped object storage, and OIDC configuration before runtime code and deployments can consume cluster services. The CopyStart repository must define what the application expects; it must not become a second owner of the mini-cluster's shared infrastructure.

## What Changes

- Define the application configuration and secret-reference contract for PostgreSQL, private object storage, OIDC, and any constrained tenant-onboarding identity.
- Define fail-closed behavior for missing or conflicting runtime configuration and readiness checks for required dependencies.
- Document the handoff to the mini-cluster changes that provision shared services, secrets, identities, backups, and bootstrap integration.
- Keep production consumption disabled until the separate protected activation decision.
- **BREAKING** Remove CopyStart ownership of PostgreSQL roles, SeaweedFS resources, OpenBao/ESO objects, shared ZITADEL resources, and mini-cluster bootstrap code.

## Capabilities

### New Capabilities
- `saas-platform-prerequisites`: Defines the runtime configuration, secret-reference, fail-closed, and readiness contract consumed by CopyStart workloads.

### Modified Capabilities

None.

## Impact

Touches CopyStart runtime configuration, validation, and operator documentation only. The mini-cluster change `provision-copystart-platform-prerequisites` owns database/storage/secret/ZITADEL provisioning and Day-1 integration; `harden-seaweedfs-s3-security` owns shared SeaweedFS access hardening. Depends on `establish-project-governance`, `remediate-legacy-secrets-and-inventory`, and the agreed tenant/OIDC contract. This change performs no cluster mutation and has no bootstrap stage of its own.