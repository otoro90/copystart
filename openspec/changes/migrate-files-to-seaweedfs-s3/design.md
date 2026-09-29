## Context

CopyStart writes request files directly with `File.Create` beneath `PathBaseFiles` and stores only basic name, extension, and size metadata. GovCore demonstrates an S3 abstraction and separate internal/external clients, while the mini-cluster already runs SeaweedFS 3.80. Current shared SeaweedFS configuration contains anonymous read and tracked test credentials, so application integration is blocked until `provision-saas-platform-prerequisites` hardens it.

## Goals / Non-Goals

**Goals:**
- Store tenant files privately through the S3 API.
- Support safe direct browser transfer without exposing credentials.
- Make file state auditable, verifiable, and migratable.

**Non-Goals:**
- Expose public buckets for tenant websites.
- Store binary content in PostgreSQL.
- Depend on SeaweedFS-specific APIs where standard S3 operations suffice.

## Decisions

1. Define `IObjectStorage` in Application and an AWS S3 SDK implementation in Infrastructure with independent internal operation and external signing clients.
2. Use one private application bucket initially and keys shaped as `{environment}/{tenantId}/{purpose}/{documentId}/{safeName}`. Bucket-per-tenant is rejected because SeaweedFS maps buckets to collections and many small buckets waste volumes.
3. Model metadata states `Pending`, `Uploaded`, `Quarantined`, `Available`, `DeletionPending`, and `Deleted`. The server owns keys and state transitions.
4. Use short-lived presigned PUT/GET URLs. Large files use multipart upload; normal uploads enforce content length, approved MIME types, checksums, and CORS origins.
5. Scan newly uploaded objects before availability. Scanner choice is deferred, but the state contract and quarantine path are mandatory.
6. Use an idempotent reconciliation worker for expired intents, missing objects, orphan objects, and pending deletions.
7. Migrate legacy files from a checksum manifest after tenant ownership is mapped; never infer tenant from a filename.

## Risks / Trade-offs

- [Presigned URL leaks] -> Short TTL, single object/verb, private bucket, no list permission, and redacted logs.
- [Shared bucket weakens isolation] -> Tenant-prefixed keys, server authorization, least-privilege identity, metadata ownership, and cross-tenant tests.
- [Browser upload bypasses API validation] -> Pending intent plus post-upload verification before availability.
- [SeaweedFS differs from AWS S3] -> Restrict usage to verified supported operations and add integration tests against the deployed version.

## Migration Plan

1. Complete SeaweedFS hardening and provision the application bucket/identity.
2. Implement metadata and disabled/test storage adapters.
3. Implement presigned lifecycle, scan boundary, and reconciliation.
4. Validate CORS, signatures, multipart behavior, and tenant denial against SeaweedFS 3.80.
5. Generate the legacy manifest, dry-run, migrate, reconcile, and retain source files through the rollback window.

Rollback disables new uploads, returns the prior application release, and preserves both source files and migrated objects. Object deletion is postponed until reconciliation and acceptance complete.

## Research Evidence

- SeaweedFS official documentation via Context7 and web, accessed 2026-09-27: supports presigned URLs, multipart upload, bucket policies, CORS, checksums, versioning, and private IAM policies; many buckets can consume volumes because each maps to a collection.
- Mini-cluster `CONTEXT.md`, accessed 2026-09-27: SeaweedFS 3.80, internal endpoint `seaweedfs-s3.storage.svc.cluster.local:8333`, external endpoint `https://s3.forjanova.com`, path-style access, and private-bucket intent.
- Local SeaweedFS manifest, inspected 2026-09-27: anonymous read and tracked test credentials require remediation before use.
- Local GovCore S3 service, inspected 2026-09-27: useful dual-endpoint pattern; CopyStart adds pending verification and quarantine rather than trusting client object keys.

## Open Questions

- Which malware scanner fits the ARM64 resource budget?
- What file-size limits do the pilot workflows actually require?
- Which document classes require retention or object lock?