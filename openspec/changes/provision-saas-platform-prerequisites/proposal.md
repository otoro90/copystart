## Why

Application development needs testable database, identity, secret, and object-storage contracts before final workload deployment. Provisioning these only in the deployment change creates a circular dependency and prevents integration testing.

## What Changes

- Provision dedicated development PostgreSQL database/role, SeaweedFS private bucket/identity, OpenBao paths/policies, ExternalSecrets, and ZITADEL shared project/app/role resources.
- Define ownership: Terraform manages shared ZITADEL resources; the application may provision tenant organizations and grants through a constrained service identity.
- Add a bounded `copystart-prerequisites` module/profile to the canonical mini-cluster Day-1 facade with check-only and dry-run behavior.
- Add idempotency, least-privilege, readiness, backup/restore, and rollback checks for every prerequisite.
- Keep production credentials and activation outside automatic bootstrap until an approved cutover.

## Capabilities

### New Capabilities
- `saas-platform-prerequisites`: Provides reproducible development data, identity, secret, and storage prerequisites for the CopyStart SaaS.

### Modified Capabilities

None.

## Impact

Touches mini-cluster Terraform, OpenBao/ESO policy, PostgreSQL and SeaweedFS provisioning, ZITADEL resources, bootstrap orchestration, and operational documentation. CopyStart owns the required contracts; mini-cluster owns shared infrastructure realization. Depends on `establish-project-governance` and `remediate-legacy-secrets-and-inventory`.