## Why

The delivery roadmap in `docs/delivery/roadmap.md` spans 13 interconnected changes across two repositories (`copystart` and `mini-cluster`). Previous execution attempts stalled because autonomous agents encountered artificial circular blocks: backend multitenancy changes stalled waiting for live ZITADEL/PostgreSQL cluster resources, cluster provisioning changes stalled waiting for SeaweedFS hardening, and agents lacked an explicit cross-repo handoff protocol, test-isolation strategy, and safe operational boundary for shared infrastructure hosting `GovCo.Tramites`. An explicit orchestration change is needed to govern the execution sequence, decouple application code verification from live cluster state, enforce blast-radius guards for shared infrastructure, and track orderly transition across all 13 gates.

## What Changes

- Introduce a canonical delivery orchestration specification and master execution runbook defining the four-phase execution pipeline across `copystart` and `mini-cluster`.
- Decouple application-level verification from live cluster availability: require in-memory test fixtures (`TestAuthenticationHandler`, mocked OIDC tokens, isolated DbContext) for `copystart` backend tasks so gates 5 and 6 can complete and archive without waiting for cluster provisioning.
- Establish strict blast-radius guards protecting `GovCo.Tramites` and existing shared infrastructure (PostgreSQL database isolation, SeaweedFS bucket preservation, ZITADEL organization boundary, OpenBao path isolation).
- Define the explicit cross-repository handoff protocol between `copystart` and `mini-cluster`.
- Update blocking prerequisites in existing changes (`add-multitenancy-and-zitadel-identity` task 2.1) to unblock autonomous implementation.
- Provide a sequential, checkpointed gate checklist tracking execution from Change 5 through Change 13.

## Capabilities

### New Capabilities
- `delivery-orchestration`: Defines the multi-repository execution protocol, test decoupling rules, cross-tenant blast-radius guards, and sequential gate criteria for the SaaS delivery roadmap.

### Modified Capabilities

None.

## Impact

Affects delivery documentation, execution sequencing, and cross-repo coordination. Does not modify application runtime code directly. Unblocks autonomous execution of `add-multitenancy-and-zitadel-identity`, `provision-saas-platform-prerequisites`, `harden-seaweedfs-s3-security`, and `provision-copystart-platform-prerequisites`. Depends on `establish-project-governance`.
