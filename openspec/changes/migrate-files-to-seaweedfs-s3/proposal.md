## Why

The prototype writes uploads directly to a local filesystem path and stores minimal metadata, which is unsafe for a replicated SaaS and cannot enforce tenant isolation consistently. The existing platform already operates SeaweedFS as its S3-compatible object store.

## What Changes

- Replace direct filesystem access with an application-level object-storage abstraction and SeaweedFS S3 implementation.
- Keep buckets private and namespace every object by environment and tenant.
- Store authoritative metadata, ownership, content type, size, checksum, scan state, and lifecycle state in PostgreSQL.
- Use short-lived presigned upload/download URLs with separate internal and external S3 endpoints.
- Create server-owned object keys and pending metadata before upload; validate object existence, size, checksum, authorization, CORS, and malware-scan outcome before making files available.
- Define orphan cleanup, quarantine, retention, and deletion semantics.
- Define a resumable legacy-file migration with reconciliation and rollback.

## Capabilities

### New Capabilities
- `tenant-object-storage`: Provides secure tenant-scoped file metadata, presigned transfer, lifecycle management, and legacy-file migration on SeaweedFS S3.

### Modified Capabilities

None.

## Impact

Replaces `PathBaseFiles` and direct `File.Create` usage, introduces S3 client dependencies, and requires a dedicated bucket identity and secret contract. Depends on `modernize-service-platform-backend` and `add-multitenancy-and-zitadel-identity`; infrastructure work is coordinated with `deploy-saas-platform-to-mini-cluster`.