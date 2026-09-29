## Context

The mini-cluster already provides K3s, Argo CD, PostgreSQL 17, OpenBao/ESO, ZITADEL, SeaweedFS 3.80, Traefik, monitoring, and backups. Its current SeaweedFS manifest includes anonymous read and committed test credentials, so it is not yet a safe foundation for tenant files. Shared ZITADEL resources and storage/database identities must exist before application integration tests, but workload deployment should remain a separate concern.

## Goals / Non-Goals

**Goals:**
- Provision secure development prerequisites reproducibly.
- Remove the storage control-plane blockers discovered during research.
- Establish clear Terraform, GitOps, bootstrap, and runtime ownership.

**Non-Goals:**
- Deploy CopyStart API or frontend workloads.
- Activate production tenants or credentials automatically.
- Replace shared PostgreSQL, SeaweedFS, ZITADEL, or OpenBao.

## Decisions

1. Add a bounded `copystart-prerequisites` profile/module to `scripts/bootstrap-full-cluster.sh`, not unconditional mutation of every full Day-1 run.
2. Stage mapping: Stage 1 validates protected bootstrap inputs; Stage 4 creates database role/database and OpenBao paths/policies; Stage 5 creates the SeaweedFS bucket/identity and applies restricted CORS; the protected post-readiness identity step reconciles ZITADEL Terraform; Stage 6 verifies all contracts.
3. Remove anonymous read and tracked credentials from SeaweedFS configuration through a separately reviewable hardening migration. Credentials originate in protected bootstrap input, are stored in OpenBao, and reach workloads through ESO.
4. Use a dedicated `copystart-dev` database/role and private `copystart-files-dev` bucket/identity. Production equivalents are declared but not populated or activated.
5. Terraform owns shared ZITADEL resources with remote state in SeaweedFS. Runtime provisioning receives only the minimum management permissions for customer organizations and grants.
6. Every helper supports `--check-only` and `--dry-run`, preserves unrelated secret fields, uses stdin/environment protected channels, and never prints secrets.
7. Extend logical database and object-storage backup contracts and require an isolated restore rehearsal.

## Risks / Trade-offs

- [Hardening breaks existing anonymous consumers] -> Inventory requests first, migrate approved public assets, then switch default-deny with a tested rollback manifest.
- [Bootstrap rotates credentials unexpectedly] -> Creation is idempotent; rotation requires an explicit flag and ownership check.
- [Shared single-node services remain a failure domain] -> Document recovery objectives, backups, and that this environment is pilot-grade until redundancy is funded.
- [Terraform/runtime both mutate ZITADEL] -> Enforce non-overlapping resource ownership and reconcile durable runtime provisioning records.

## Migration Plan

1. Inventory SeaweedFS consumers and remediate tracked credentials.
2. Add database, OpenBao/ESO, and SeaweedFS prerequisite modules with check-only tests.
3. Add shared ZITADEL Terraform resources and remote-state safeguards.
4. Integrate the bounded profile into the canonical facade.
5. Run idempotency, readiness, backup, restore, and rollback rehearsals.
6. Publish only development contracts to the application team.

Rollback uses Git revert plus Argo CD reconciliation for declarative resources, targeted Terraform restoration for shared identity, and credential overlap only with newly protected values. Buckets and databases use retain semantics and are not deleted by rollback.

## Research Evidence

- Mini-cluster `CONTEXT.md`, `SYSTEM.md`, and `bootstrap-full-cluster.sh`, inspected 2026-09-27: canonical six-stage Day-1 facade, protected post-bootstrap identity, SeaweedFS stage 5, OpenBao/ESO, and mandatory readiness/rollback contracts.
- SeaweedFS official documentation via Context7 and web, accessed 2026-09-27: production access control requires default deny and persistent credentials/IAM; CORS should allow exact trusted origins.
- ZITADEL official B2B documentation via Context7 and web, accessed 2026-09-27: shared projects and Project Grants are the intended B2B model.
- Local manifest inspection, 2026-09-27: current S3 configuration has anonymous read and tracked test credentials.

## Open Questions

- Which current SeaweedFS objects rely on anonymous access?
- Should CopyStart use static S3 credentials through ESO first or SeaweedFS STS with Kubernetes service accounts?
- What pilot recovery objectives are affordable on the single active node?