## Why

Offering the platform economically requires product plans and enforceable feature access even before automated recurring billing is selected. Coupling authorization directly to a payment provider would make pricing experiments and Colombian payment options difficult to evolve.

## What Changes

- Introduce provider-neutral plans, prices, subscriptions, billing accounts, entitlements, usage limits, trials, grace periods, and lifecycle states.
- Establish a canonical capability registry and enforce effective access server-side as the intersection of vertical-module availability, commercial entitlement, and actor authorization.
- Support manual invoicing and platform-admin activation for the first pilots.
- Define a payment-provider adapter boundary; add a durable idempotent webhook inbox only when an automated provider is selected.
- Keep payment credentials and sensitive payment data outside the application database.

## Capabilities

### New Capabilities
- `subscription-entitlements`: Provides plan assignment, subscription lifecycle, feature entitlements, usage limits, and provider-neutral billing integration boundaries.

### Modified Capabilities

None.

## Impact

Adds platform-owned commercial data and enforcement to API and administration surfaces. It does not require automated charging for the pilot release. Depends on `add-multitenancy-and-zitadel-identity`; automated provider integration remains gated by commercial validation.