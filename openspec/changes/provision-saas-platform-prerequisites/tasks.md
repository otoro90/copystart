## 1. Prerequisite Verification

- [ ] 1.1 Verify governance, local prototype credential remediation, agreed tenant/OIDC contracts, and the mini-cluster prerequisite and SeaweedFS-hardening changes
- [ ] 1.2 Confirm this CopyStart change contains no cluster manifests, Terraform, bootstrap scripts, or secret values

## 2. Runtime Configuration Contract

- [ ] 2.1 Define the application-facing PostgreSQL connection and protected credential reference
- [ ] 2.2 Define OIDC issuer/client/audience settings and protected client or onboarding credential references
- [ ] 2.3 Define private S3 endpoint/bucket/region settings and tenant-scoped authorization expectations without platform resource creation
- [ ] 2.4 Ensure secrets are absent from Git, image layers, logs, and client request DTOs; verify missing settings fail closed

## 3. Application Verification

- [ ] 3.1 Add configuration validation and dependency-specific readiness for PostgreSQL, OIDC, and authenticated object storage
- [ ] 3.2 Add tenant-isolation tests for database rows and objects, including denial of cross-tenant access without metadata disclosure
- [ ] 3.3 Add tests proving missing or unauthorized dependencies do not report the application ready and diagnostics redact secrets
- [ ] 3.4 Keep production configuration empty and production activation disabled in all CopyStart-owned overlays

## 4. Documentation and Validation

- [ ] 4.1 Document the runtime contract, secret ownership, readiness semantics, and mini-cluster handoff
- [ ] 4.2 Run focused configuration, tenant-isolation, and readiness tests
- [ ] 4.3 Run `openspec validate provision-saas-platform-prerequisites --strict`