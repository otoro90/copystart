# Security Baseline

- Secrets come from OpenBao through ESO in the cluster and from user secrets locally. Never from tracked files.
- Tracked legacy credentials are compromised and must be removed from current configuration. Rotate any credential that remains active or was reused; if its owner confirms it is retired and unused, record that rotation is not applicable. Never reactivate or reuse an exposed value. Git history remains unchanged unless a separate coordinated history-purge decision is approved.
- Tenant context never comes from client-controlled input for authenticated operations.
- Buckets are private. Presigned URLs are short-lived, single-object, and never logged.
- Payment data is never stored; only provider references.
- OWASP Top 10 review for every endpoint that accepts input or files.
