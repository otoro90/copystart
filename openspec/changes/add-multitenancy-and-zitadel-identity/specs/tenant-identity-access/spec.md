## ADDED Requirements

### Requirement: Tenant context is authoritative and fail-closed
Authenticated requests MUST derive tenant context from validated ZITADEL organization authorization, and MUST reject missing, inactive, conflicting, or unauthorized tenant context before accessing tenant-owned resources.

#### Scenario: Authenticated user changes the tenant header
- **WHEN** a valid user sends a host or header for a tenant not authorized by the token context
- **THEN** the API rejects the request and performs no tenant-owned query or mutation

### Requirement: Anonymous tenant discovery is bounded
Anonymous public endpoints SHALL resolve an active tenant from an allowlisted hostname mapping and SHALL expose only explicitly public configuration and content.

#### Scenario: Unknown subdomain requests public configuration
- **WHEN** the hostname has no active tenant-domain mapping
- **THEN** the API returns not found without falling back to another tenant

### Requirement: Tenant data is isolated in depth
Every tenant-owned aggregate, unique constraint, relationship, query, cache key, object key, background job, and audit record MUST carry or derive tenant ownership, with application filters and PostgreSQL row-level protection tested across tenants.

#### Scenario: Repository query omits an explicit tenant predicate
- **WHEN** it executes under tenant A's scoped context
- **THEN** database and ORM safeguards prevent rows owned by tenant B from being returned

### Requirement: ZITADEL provisioning is durable and idempotent
The platform SHALL use one shared product project per environment, one organization and Project Grant per customer tenant, shared SPA/API applications, and durable provisioning state with retry and compensation.

#### Scenario: Onboarding retries after organization creation
- **WHEN** a previous attempt created the ZITADEL organization but failed before persisting a complete grant
- **THEN** retry resumes or compensates without creating duplicate organizations or activating an incomplete tenant

### Requirement: Authorization separates platform and tenant authority
The platform MUST distinguish platform operators, tenant administrators, dispatchers, technicians, and customers, and MUST enforce permissions in the API independently of frontend visibility.

#### Scenario: Tenant administrator calls a platform operation
- **WHEN** the user lacks the required platform authorization
- **THEN** the API denies access even if the route or control is manually invoked

### Requirement: Identity offboarding preserves business audit
Disabling a tenant or membership SHALL revoke future access without erasing historical actor identifiers required for business and security audit.

#### Scenario: Technician membership is removed
- **WHEN** the identity no longer has tenant access
- **THEN** new requests are denied while completed work-order history retains a non-secret actor reference