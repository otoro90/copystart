## 1. Credential Remediation

- [ ] 1.1 Inventory tracked and historical credential locations with redacted identifiers and named owners
- [ ] 1.2 Rotate each exposed credential through an authorized operator and verify the prior value is rejected
- [ ] 1.3 Replace tracked secrets with inert examples or protected configuration references
- [ ] 1.4 Add local and CI secret scanning and prove it rejects a synthetic credential without leaking it

## 2. Legacy Inventory

- [ ] 2.1 Create read-only database and filesystem inventory tooling with documented prerequisites
- [ ] 2.2 Capture checksummed source snapshots and restricted inventory reports
- [ ] 2.3 Rerun the inventory against an unchanged snapshot and verify deterministic results
- [ ] 2.4 Record data-quality exceptions, migration blockers, preservation rules, and rollback ownership

## 3. Verification

- [ ] 3.1 Verify the legacy application uses only replacement secret channels where it must remain runnable
- [ ] 3.2 Run repository secret scanning, inventory tests, and `openspec validate remediate-legacy-secrets-and-inventory --strict`