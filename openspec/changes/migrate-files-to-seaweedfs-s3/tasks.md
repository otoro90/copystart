## 1. Storage Contract

- [ ] 1.1 Verify private SeaweedFS bucket, identity, CORS, endpoints, backup, and restore prerequisites are ready
- [ ] 1.2 Define tenant-owned document metadata, upload intent, lifecycle states, retention, and audit contracts
- [ ] 1.3 Implement disabled/test and SeaweedFS S3 adapters with separate internal and external clients
- [ ] 1.4 Add server-owned object-key generation and tenant authorization tests

## 2. Verified Transfer Lifecycle

- [ ] 2.1 Implement pending upload intent and short-lived presigned PUT/GET endpoints
- [ ] 2.2 Verify object existence, declared size/type, checksum, and scan state before availability
- [ ] 2.3 Implement quarantine and the selected ARM64-compatible malware-scanning adapter
- [ ] 2.4 Implement idempotent reconciliation for expired intents, missing/orphan objects, and pending deletion
- [ ] 2.5 Test CORS, signature expiry, multipart upload, cross-tenant denial, and redacted logging against SeaweedFS 3.80

## 3. Legacy Migration

- [ ] 3.1 Generate a tenant-mapped file manifest from the approved legacy inventory
- [ ] 3.2 Run resumable dry-run and isolated migration rehearsals with checksum reconciliation
- [ ] 3.3 Execute cutover only after reconciliation, preserving source files through the rollback window
- [ ] 3.4 Verify backup/restore and run strict OpenSpec validation