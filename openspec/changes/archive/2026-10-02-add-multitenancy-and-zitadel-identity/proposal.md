## Why

CopyStart currently uses one database context and local ASP.NET Identity without tenant ownership. A SaaS offering requires fail-closed tenant isolation and delegated business identity before onboarding multiple companies.

## What Changes

- Introduce tenant, domain, membership, identity mapping, role, and provisioning-state models.
- Add tenant ownership to tenant-scoped aggregates, constraints, object namespaces, caches, jobs, and audit records.
- Resolve anonymous site context from verified host configuration and authenticated tenant context exclusively from validated ZITADEL organization claims; unresolved or conflicting authenticated context fails closed.
- Adopt one ZITADEL organization per customer, one shared project per product/environment, shared SPA/API applications, and Project Grants.
- Define idempotent onboarding with durable provisioning state, compensation, offboarding, cross-tenant tests, and explicit platform-administrator delegation.
- Persist ZITADEL organization, project-grant, and provisioning-state identifiers without storing identity-provider secrets.
- Remove local password ownership from the application after migration.

## Capabilities

### New Capabilities
- `tenant-identity-access`: Provides tenant discovery, isolation, ZITADEL authentication, memberships, authorization, onboarding, and offboarding boundaries.

### Modified Capabilities

None.

## Impact

Changes every tenant-owned persistence and request path and introduces ZITADEL provisioning dependencies. It adapts GovCore's organization/project-grant approach while removing permissive fallbacks, government-specific roles, and incomplete provisioning behavior. Its tenant ownership model must be accepted before the backend target schema and legacy migration are finalized.