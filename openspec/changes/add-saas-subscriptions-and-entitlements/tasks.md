## 1. Commercial Domain

- [ ] 1.1 Validate pilot pricing, billing frequency, trial, grace, suspension, and cancellation policies with prospective customers
- [ ] 1.2 Define provider-neutral plan versions, prices, billing accounts, subscriptions, entitlements, and usage limits
- [ ] 1.3 Implement the shared effective-capability evaluator across template availability, entitlement, and actor authorization
- [ ] 1.4 Add server-side capability and limit enforcement with cross-tenant and client-tampering tests

## 2. Pilot Billing

- [ ] 2.1 Implement audited manual subscription activation, renewal, grace, suspension, cancellation, and reactivation
- [ ] 2.2 Expose read-only effective capability and subscription state to tenant administrators
- [ ] 2.3 Verify suspended tenants retain permitted read/export access according to policy

## 3. Provider Decision Gate

- [ ] 3.1 Compare Stripe Billing, Wompi recurring sources, and continued manual invoicing against Colombian legal/commercial requirements
- [ ] 3.2 Record the selected provider and exact event/state mapping in a separate ADR before integration
- [ ] 3.3 If automated billing is approved, implement the adapter, signature verification, idempotent inbox, reconciliation, and secret custody
- [ ] 3.4 Test duplicate, delayed, failed, reordered, and forged provider events and run strict OpenSpec validation