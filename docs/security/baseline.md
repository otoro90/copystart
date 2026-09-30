# Security Baseline

- Secrets come from OpenBao through ESO in the cluster and from user secrets locally. Never from tracked files.
- Tracked legacy credentials are compromised and must be rotated, not merely removed.
- Tenant context never comes from client-controlled input for authenticated operations.
- Buckets are private. Presigned URLs are short-lived, single-object, and never logged.
- Payment data is never stored; only provider references.
- OWASP Top 10 review for every endpoint that accepts input or files.
