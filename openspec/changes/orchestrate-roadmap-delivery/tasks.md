## 1. Prerequisite Verification and Master Runbook

- [x] 1.1 Verify governance, roadmap gates in `docs/delivery/roadmap.md`, and current states of `copystart` and `mini-cluster` changes
- [x] 1.2 Create the master execution runbook at `docs/delivery/master-execution-runbook.md` detailing the four-phase pipeline, handoffs, and verification commands
- [x] 1.3 Update task 2.1 in `openspec/changes/add-multitenancy-and-zitadel-identity/tasks.md` to explicitly decouple from live cluster and use in-memory `TestAuthenticationHandler`
- [x] 1.4 Add tenant-isolation and cross-tenant denial test rules to the master runbook checklist

## 2. Phase 1 Execution Gates (CopyStart Backend Core)

- [x] 2.1 Complete and verify all remaining tasks in `add-multitenancy-and-zitadel-identity` using in-memory test fixtures, then run `openspec validate add-multitenancy-and-zitadel-identity --strict`
- [x] 2.2 Archive change `add-multitenancy-and-zitadel-identity` upon passing test suite
- [x] 2.3 Implement and verify runtime configuration options and fail-closed checks in `provision-saas-platform-prerequisites`
- [x] 2.4 Validate and archive `provision-saas-platform-prerequisites`

## 3. Phase 2 Execution Gates (Mini-Cluster Infrastructure)

- [ ] 3.1 Switch context to `mini-cluster` repository and execute `harden-seaweedfs-s3-security` preserving `tramites-app` bucket credentials
- [ ] 3.2 Validate and archive `harden-seaweedfs-s3-security`
- [ ] 3.3 Execute `provision-copystart-platform-prerequisites` provisioning `copystart_dev_db`, `copystart-dev-files` bucket, and isolated ZITADEL Org
- [ ] 3.4 Validate and archive `provision-copystart-platform-prerequisites`

## 4. Phase 3 and Phase 4 Roadmap Alignment

- [ ] 4.1 Switch context to `copystart` and execute `build-angular-multisurface-frontend` and `add-versioned-vertical-templates`
- [ ] 4.2 Execute `migrate-files-to-seaweedfs-s3`, `add-saas-subscriptions-and-entitlements`, and `deploy-saas-platform-to-mini-cluster`
- [ ] 4.3 Run `openspec validate orchestrate-roadmap-delivery --strict`
