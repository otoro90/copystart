## 1. Prerequisite Verification

- [ ] 1.1 Confirm governance prerequisites and enumerate only tracked prototype configuration and the tracked PNG, without printing credential values
- [ ] 1.2 Identify the authorized owner for the historical PostgreSQL credential and ask whether an authoritative database or external upload snapshot exists

## 2. Local Credential and File Evidence

- [ ] 2.1 Replace any tracked live prototype credential with an inert example or local-only secret reference
- [ ] 2.2 If the owner confirms a credential is active or reused, rotate it out of band and verify rejection of its previous value; otherwise record that rotation is not applicable
- [ ] 2.3 Record the tracked PNG path, size, media type, and SHA-256 without treating it as a complete upload inventory
- [ ] 2.4 Assess Git history for redacted exposure evidence; do not rewrite history under this change

## 3. Conditional Migration Inventory

- [ ] 3.1 If an owner-approved immutable database or external file snapshot exists, capture a read-only structural/checksum inventory and keep restricted reports outside Git; otherwise record the source as unavailable/not applicable
- [ ] 3.2 Test that the inventory is deterministic, non-mutating, and cannot query or export data outside its explicitly selected legacy source
- [ ] 3.3 Verify there is no tenant-owned SaaS data in scope; if the source is later identified as tenant-owned, require approved tenant-scoped access and isolation tests before inventory
- [ ] 3.4 Run focused inventory checks and `openspec validate remediate-legacy-secrets-and-inventory --strict`