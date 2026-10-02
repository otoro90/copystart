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
2. Mini-cluster protected Terraform owns shared project, applications, actions, role definitions, and the infrastructure provisioning identity. CopyStart runtime may create tenant organizations and grants only through a separately constrained identity, persisting `OrganizationId`, `ProjectGrantId`, state, attempt, and error metadata.
3. Resolve anonymous tenant context from an exact `SiteDomain` record. Resolve authenticated context from validated organization/project authorization; host and optional request headers may select only among already authorized memberships.
4. Fail closed for protected endpoints. Global health, discovery, onboarding callback, and public-site endpoints are explicitly classified rather than inferred from a missing tenant.
5. Use layered isolation: tenant-aware aggregate keys and unique indexes, EF Core query filters, PostgreSQL RLS, tenant-qualified caches/object keys, and explicit tenant envelopes for background processing.
6. Keep application permissions in a canonical registry mapped to ZITADEL project roles. Frontend guards improve UX but never authorize API behavior.
7. Migrate local users by verified email invitation/account linking; never migrate password hashes to ZITADEL.

## Accepted Contracts

- A person may hold memberships in multiple tenant organizations from the first release. A request's active organization must be selected deliberately from memberships already authorized by the validated identity; email domains, hostnames, and client-supplied tenant identifiers never create membership or authority.
- The initial role set is `platform-operator` (platform operations only), `tenant-administrator`, `dispatcher`, `technician`, and `customer`. Platform operations use distinct endpoints and permissions; tenant roles cannot grant platform authority. The canonical capability registry defines API permissions and maps tenant roles to ZITADEL project roles. Tenant template and subscription gates remain independent of actor permissions.
- Tenant membership records bind the internal person/account to a tenant and retain status, role assignments, and stable external identity references. Identity mapping uses the validated ZITADEL issuer and subject pair; email is an invitation/account-linking attribute, not a durable identity key. Revocation disables future authorization without deleting historical actor references.
- Tenant-owned rows carry tenant ownership. Tenant-scoped unique keys and relationships include the tenant key, and cross-tenant relationships are rejected. Domain mappings are exact normalized hostnames with an active state; wildcard or suffix matching does not select a tenant.
- Runtime database access uses a non-owner role without `BYPASSRLS`; tenant context is set transaction-locally for each unit of work. RLS policies apply to tenant-owned tables, including table owners where supported via `FORCE ROW LEVEL SECURITY`. Schema migrations use a separate deployment identity. Administrative exports use a separate explicitly authorized and audited path, never the application runtime role. Connection-pool reuse must not retain tenant context.
- Caches, object keys, background-job envelopes, and audit records carry or derive the tenant identifier. Background work restores and validates tenant context before accessing tenant-owned data; actor references are stable and contain no credentials or tokens.

## Risks / Trade-offs

- [Connection pooling leaks PostgreSQL tenant session state] -> Set and clear transaction-local tenant context and test pooled connections across tenants.
- [ZITADEL and database provisioning diverge] -> Use a durable state machine, idempotency keys, reconciliation, and compensating deletion only when ownership is certain.
- [Users need multiple businesses] -> Model memberships explicitly and require deliberate organization selection; do not infer from email domain.
- [Identity outage blocks operations] -> Cache only validated sessions within token lifetime and document degraded behavior without bypassing authorization.

## Migration Plan

1. Define tenant ownership and capability registry before final backend migrations.
2. Have mini-cluster `provision-copystart-platform-prerequisites` provision shared ZITADEL resources; consume the application runtime contract from CopyStart `provision-saas-platform-prerequisites`.
3. Add tenant schema, constraints, query filters, RLS, and cross-tenant tests.
4. Implement token validation and anonymous host discovery.
5. Implement durable onboarding and membership administration.
6. Invite/link legacy users, verify roles, then disable local sign-in.

Rollback disables new tenant onboarding, restores the prior application release, and retains provisioned organizations for reconciliation. Tenant records are never merged or reassigned during rollback.

## Research Evidence

- ZITADEL official B2B documentation via Context7 and web, accessed 2026-09-27: organizations represent partners; Project Grants share one project and restrict available roles; organization selection can be carried in reserved OIDC scopes.
- Local GovCore `TenantMiddleware`, `TenantService`, and tests, inspected 2026-09-27: useful claim and subdomain patterns plus permissive fallbacks that must not be copied.
- Independent decision verification, 2026-09-27: required durable grant state, fail-closed resolution, and clear Terraform/application ownership.

## Resolved Questions

- Multiple customer organizations per person are supported in the first release, with deliberate selection among authorized memberships.
- The initial roles are platform operator, tenant administrator, dispatcher, technician, and customer; the capability registry is authoritative for permission mapping.
- The runtime role is non-owner and has no `BYPASSRLS`; migrations and administrative exports use distinct identities, with exports explicitly authorized and audited. Tenant context is transaction-local.