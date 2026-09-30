---
name: 'Object storage'
description: 'Use when uploading, downloading, deleting, or migrating files, or when using S3 or SeaweedFS.'
---
# Object Storage Rules

- The server generates object keys: `{environment}/{tenantId}/{purpose}/{documentId}/{safeName}`. Never accept a client key.
- Create pending metadata before issuing a presigned URL. Mark available only after size, type, checksum, and scan verification.
- Backend calls use the internal endpoint; browser signatures use the external endpoint. Path-style addressing.
- Presigned URLs are short-lived, single object and verb, and never logged.
- Do not create a bucket per tenant.
