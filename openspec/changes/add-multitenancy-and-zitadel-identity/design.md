## Context

CopyStart combines local ASP.NET Identity and business data in one context and has no tenant ownership. GovCore demonstrates host discovery, ZITADEL claims, and one organization per customer, but its middleware allows unresolved requests to continue and its provisioning path has known compensation gaps. The new system must adapt the pattern and fail closed.

## Goals / Non-Goals

**Goals:**
- Provide B2B identity and tenant isolation suitable for unrelated small businesses.
- Make authenticated identity claims authoritative while supporting anonymous branded sites.
- Automate onboarding safely and preserve auditability.

**Non-Goals:**
- Create a separate ZITADEL project or OIDC client for every customer.
- Trust client-provided tenant IDs for authenticated business operations.
- Reuse government roles or legacy tenant claims.

## Decisions

1. Use one ZITADEL organization per customer, one shared product project per environment, shared SPA/API applications, and a Project Grant to each customer organization.
2. Terraform owns shared project, applications, actions, and role definitions. A constrained provisioning service may own tenant organizations and grants, persisting `OrganizationId`, `ProjectGrantId`, state, attempt, and error metadata.
3. Resolve anonymous tenant context from an exact `SiteDomain` record. Resolve authenticated context from validated organization/project authorization; host and optional request headers may select only among already authorized memberships.
4. Fail closed for protected endpoints. Global health, discovery, onboarding callback, and public-site endpoints are explicitly classified rather than inferred from a missing tenant.
5. Use layered isolation: tenant-aware aggregate keys and unique indexes, EF Core query filters, PostgreSQL RLS, tenant-qualified caches/object keys, and explicit tenant envelopes for background processing.
6. Keep application permissions in a canonical registry mapped to ZITADEL project roles. Frontend guards improve UX but never authorize API behavior.
7. Migrate local users by verified email invitation/account linking; never migrate password hashes to ZITADEL.

## Risks / Trade-offs

- [Connection pooling leaks PostgreSQL tenant session state] -> Set and clear transaction-local tenant context and test pooled connections across tenants.
- [ZITADEL and database provisioning diverge] -> Use a durable state machine, idempotency keys, reconciliation, and compensating deletion only when ownership is certain.
- [Users need multiple businesses] -> Model memberships explicitly and require deliberate organization selection; do not infer from email domain.
- [Identity outage blocks operations] -> Cache only validated sessions within token lifetime and document degraded behavior without bypassing authorization.

## Migration Plan

1. Define tenant ownership and capability registry before final backend migrations.
2. Provision shared ZITADEL resources through `provision-saas-platform-prerequisites`.
3. Add tenant schema, constraints, query filters, RLS, and cross-tenant tests.
4. Implement token validation and anonymous host discovery.
5. Implement durable onboarding and membership administration.
6. Invite/link legacy users, verify roles, then disable local sign-in.

Rollback disables new tenant onboarding, restores the prior application release, and retains provisioned organizations for reconciliation. Tenant records are never merged or reassigned during rollback.

## Research Evidence

- ZITADEL official B2B documentation via Context7 and web, accessed 2026-09-27: organizations represent partners; Project Grants share one project and restrict available roles; organization selection can be carried in reserved OIDC scopes.
- Local GovCore `TenantMiddleware`, `TenantService`, and tests, inspected 2026-09-27: useful claim and subdomain patterns plus permissive fallbacks that must not be copied.
- Independent decision verification, 2026-09-27: required durable grant state, fail-closed resolution, and clear Terraform/application ownership.

## Open Questions

- Can one natural person belong to multiple customer organizations in the first release?
- Which tenant roles are required by the two pilot businesses?
- Which PostgreSQL role sets RLS context for migrations and administrative exports?