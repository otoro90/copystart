## 1. Preflight and Ownership

- [ ] 1.1 Verify legacy credential remediation and inventory all current SeaweedFS anonymous consumers
- [ ] 1.2 Document CopyStart/mini-cluster/Terraform/runtime ownership and the exact Day-1 stage map
- [ ] 1.3 Add failing contract tests for missing protected inputs, default-deny S3, duplicate provisioning, and check-only mutation

## 2. Data, Secrets, and Storage

- [ ] 2.1 Add idempotent PostgreSQL development database/role provisioning and readiness checks
- [ ] 2.2 Add OpenBao paths/policies and ESO resources without plaintext credentials or unrelated-field replacement
- [ ] 2.3 Migrate SeaweedFS from tracked test credentials and anonymous read to protected default-deny configuration
- [ ] 2.4 Add the private CopyStart bucket/identity, exact-origin CORS, quotas/lifecycle, and authenticated smoke tests

## 3. Shared Identity

- [ ] 3.1 Add Terraform for the shared development ZITADEL project, SPA/API applications, role registry, and constrained provisioning identity
- [ ] 3.2 Configure protected remote state and prove plan/apply idempotency without secret output
- [ ] 3.3 Verify runtime ownership cannot mutate shared project resources outside its delegated scope

## 4. Day-1 and Recovery

- [ ] 4.1 Add the bounded `copystart-prerequisites` profile/module to `bootstrap-full-cluster.sh`
- [ ] 4.2 Implement preflight, dry-run, check-only, idempotent apply, readiness, and explicit rotation behavior
- [ ] 4.3 Extend backup contracts and complete isolated database/object restore rehearsal
- [ ] 4.4 Exercise GitOps/Terraform rollback while retaining databases and buckets
- [ ] 4.5 Update mini-cluster `CONTEXT.md` and detailed docs and run strict OpenSpec validation in both repositories