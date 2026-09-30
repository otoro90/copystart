---
name: 'Multitenancy and identity'
description: 'Use when handling tenant context, authentication, authorization, ZITADEL, or any query over tenant-owned data.'
applyTo: 'src/**/{Tenancy,Identity,Auth,Security}/**'
---
# Multitenancy Rules

- Authenticated tenant context comes only from validated ZITADEL organization claims. Client headers such as `X-Tenant-Id` are never trusted; reject a mismatch with 403.
- Anonymous public requests resolve tenant only from an exact active host mapping. Unknown hosts return 404.
- Missing, inactive, or conflicting tenant context fails closed. Never fall back to "all tenants".
- Platform-admin cross-tenant reads use separate `/admin/` endpoints, explicit platform roles, and audit logs.
- Tenant ownership applies to entities, unique indexes, caches, object keys, background jobs, and audit records.
- Every tenant-owned query path needs a two-tenant isolation test, including a forged header case.
- Request DTOs never contain `tenantId`.
