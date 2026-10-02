## 1. Tenant and Capability Model

- [x] 1.1 Finalize tenant, domain, membership, identity mapping, role, permission, and capability-registry contracts
- [x] 1.2 Add tenant ownership, composite uniqueness, foreign-key, audit, cache, job, and object-key rules to the target schema
- [x] 1.3 Write cross-tenant failing tests before implementing ORM filters and PostgreSQL RLS
- [x] 1.4 Implement scoped tenant context with transaction-local database enforcement and pooled-connection cleanup tests

## 2. ZITADEL Integration

- [x] 2.1 Define typed ZITADEL OIDC options and consume mock authentication identity (`TestAuthenticationHandler`) decoupled from physical cluster provisioning
- [x] 2.2 Implement JWT validation and fail-closed authenticated tenant resolution
- [x] 2.3 Implement exact-host anonymous tenant discovery and explicitly classified public/global endpoints
- [x] 2.4 Implement durable idempotent organization/grant provisioning with reconciliation and compensation
- [x] 2.5 Implement memberships, role assignment, deliberate multi-organization selection if approved, and platform delegation

## 3. Identity Migration and Verification

- [x] 3.1 Map legacy users to invitations or verified account links without migrating password hashes
- [x] 3.2 Test inactive tenants, conflicting hosts, forged headers, cross-tenant IDs, background jobs, caches, and audit records
- [x] 3.3 Verify onboarding retry, partial-failure recovery, membership revocation, and offboarding behavior
- [x] 3.4 Disable local sign-in only after migrated-user acceptance checks pass
- [x] 3.5 Run backend/frontend auth tests and strict OpenSpec validation without logging tokens or secrets