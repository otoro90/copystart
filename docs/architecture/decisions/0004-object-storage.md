# ADR-0004: Private Object Storage on SeaweedFS

- Status: Accepted, blocked on SeaweedFS hardening.
- Date: 2026-09-27.

## Decision

- One private bucket per environment with keys `{environment}/{tenantId}/{purpose}/{documentId}/{safeName}`. Bucket-per-tenant is rejected because each SeaweedFS bucket is a collection that reserves volumes.
- Server creates pending metadata and the object key, then issues a short-lived presigned URL through the external endpoint.
- Objects become available only after size, type, checksum, and malware-scan verification.
- Backend operations use `seaweedfs-s3.storage.svc.cluster.local:8333`; browser signatures use `https://s3.forjanova.com` with path-style addressing.

## Sources

- https://github.com/seaweedfs/seaweedfs/wiki/Amazon-S3-API (accessed 2026-09-27)
- https://github.com/seaweedfs/seaweedfs/wiki/S3-CORS (accessed 2026-09-27)
