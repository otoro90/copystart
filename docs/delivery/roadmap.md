# Delivery Roadmap

Apply changes in this order. Each gate must pass before the next change starts implementation.

| # | Change | Gate to proceed |
| --- | --- | --- |
| 1 | `remediate-legacy-secrets-and-inventory` | Credentials rotated; deterministic inventory |
| 2 | `establish-project-governance` | Docs, instructions, strict validation |
| 3 | `modernize-service-platform-backend` (skeleton + domain) | Domain tests pass; persistence frozen until #4 contract |
| 4 | `add-multitenancy-and-zitadel-identity` (contracts) | Tenant ownership and capability registry accepted |
| 5 | `provision-saas-platform-prerequisites` | Default-deny S3, dev DB, ZITADEL project, Day-1 profile |
| 6 | `build-angular-multisurface-frontend` | Spike passes budgets |
| 7 | `add-versioned-vertical-templates` | Copier and automotive scenarios pass |
| 8 | `migrate-files-to-seaweedfs-s3` | Cross-tenant denial and verified upload lifecycle |
| 9 | `add-saas-subscriptions-and-entitlements` | Pilot pricing validated |
| 10 | `deploy-saas-platform-to-mini-cluster` | Restore and rollback rehearsed; production stays disabled |

Changes 3 and 4 are refined together because the target schema depends on tenant ownership.
