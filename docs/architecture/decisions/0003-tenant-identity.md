# ADR-0003: Tenant Identity with ZITADEL

- Status: Accepted.
- Date: 2026-09-27.

## Decision

- One ZITADEL organization per tenant, one shared product project per environment, shared SPA/API applications, and one Project Grant per tenant organization.
- Terraform owns the shared project, applications, and roles. The runtime provisioning identity owns only tenant organizations and grants, with durable, idempotent provisioning state.
- Authenticated tenant context comes from validated organization claims. Anonymous public context comes from an exact host-to-tenant mapping.
- Missing, inactive, or conflicting tenant context fails closed. Platform-admin access uses separate endpoints and explicit roles.

## Rejected

- Realm or OIDC client per tenant.
- Client-supplied tenant IDs for authenticated operations.
- GovCore's fallback of continuing when no tenant resolves.

## Sources

- https://zitadel.com/docs/guides/solution-scenarios/b2b (accessed 2026-09-27)
