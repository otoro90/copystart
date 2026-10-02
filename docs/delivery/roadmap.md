# Delivery Roadmap

Apply changes in this order. Each gate must pass before the next change starts implementation.

| # | Change / Owner | Gate to proceed |
| --- | --- | --- |
| 1 | `remediate-legacy-secrets-and-inventory` (CopyStart) | Tracked prototype values removed; active credential use checked; import inventory completed only for an owner-approved source, otherwise recorded not applicable |
| 2 | `establish-project-governance` (CopyStart, archived 2026-09-30) | Passed: docs, path-scoped instructions, governance checks, and strict validation |
| 3 | `establish-github-ci-and-image-publishing` (CopyStart) | Read-only PR checks established; image publication waits for actual .NET/Angular targets and Dockerfiles; CI has no cluster credentials |
| 4 | `modernize-service-platform-backend` (CopyStart, archived 2026-09-30) | Passed: .NET 10 modular monolith skeleton, dependency audit, CPM pins, domain & architecture tests pass; persistence waits for #5; source import recorded not applicable |
| 5 | `add-multitenancy-and-zitadel-identity` (CopyStart, archived 2026-10-02) | Passed: Tenant ownership (ITenantOwned), RLS policies, EF Core global filters, ClaimsTenantContext, ZITADEL org claims, and hermetic in-memory test suites pass |
| 6 | `provision-saas-platform-prerequisites` (CopyStart runtime contract, archived 2026-10-02) | Passed: Strongly typed PostgreSqlOptions, StorageOptions, ZitadelOptions, readiness health checks, and fail-closed validation verified without cluster mutation |
| 7 | `harden-seaweedfs-s3-security` (mini-cluster, archived 2026-10-02) | Passed: all consumers inventoried; one OpenBao/ESO owner; anonymous access denied; existing objects and required consumers verified |
| 8 | `provision-copystart-platform-prerequisites` (mini-cluster, archived 2026-10-02) | Passed: development DB/role, private bucket identity, protected secrets, shared ZITADEL resources, backup/restore, and Stage 6 readiness pass; production remains absent |
| 9 | `build-angular-multisurface-frontend` (CopyStart) | Spike passes budgets |
| 10 | `add-versioned-vertical-templates` (CopyStart) | Copier and automotive scenarios pass |
| 11 | `migrate-files-to-seaweedfs-s3` (CopyStart) | Cross-tenant denial and verified upload lifecycle; migration only if an approved legacy source exists |
| 12 | `add-saas-subscriptions-and-entitlements` (CopyStart) | Pilot pricing validated |
| 13 | `deploy-saas-platform-to-mini-cluster` (CopyStart + mini-cluster) | CI digests, workload overlays, Argo integration, ingress, restore, and rollback rehearsed; production stays disabled |

Changes 4 and 5 are refined together because the target schema depends on tenant ownership. Changes 7 and 8 are mini-cluster infrastructure owners; CopyStart defines consumption and workload manifests only. Legacy data import and production activation remain separate, explicit decisions.
