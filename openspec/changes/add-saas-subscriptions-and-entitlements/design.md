## Context

The product needs a sellable plan model, but the first two pilots do not justify coupling launch to automated recurring billing. Stripe provides a complete subscription and entitlement lifecycle; Wompi supports tokenized payment sources and unattended future charges in Colombia but leaves more billing orchestration to the application. The architecture should support manual invoicing first and defer provider selection.

## Goals / Non-Goals

**Goals:**
- Model commercial access independently of payment processing.
- Enable low-risk paid pilots and future automated billing.
- Keep entitlements consistent with templates and actor permissions.

**Non-Goals:**
- Build accounting, tax, or electronic invoicing software.
- Store sensitive payment credentials.
- Select Stripe or Wompi before legal, currency, fee, and customer-payment validation.

## Decisions

1. Separate `PlanVersion`, `Price`, `Subscription`, `Entitlement`, `UsageCounter`, and `BillingProviderReference`. Plans and entitlements are immutable by version.
2. Start with manual invoice/activation and platform-admin controls. This validates willingness to pay without making payment integration critical path.
3. Compute effective capabilities through one registry shared with vertical templates and authorization. Cache results by tenant/configuration version but enforce them in the API.
4. Define a provider adapter for checkout/customer/subscription references and verified lifecycle events. Do not create a webhook inbox until a provider is chosen and concrete events exist.
5. When automated billing is added, store webhook payload hashes and minimal redacted payloads, verify signatures before acknowledgment, process idempotently, and separate payment state from access policy.
6. Apply a documented grace policy rather than instantly deleting data or disabling all reads after payment failure.

## Risks / Trade-offs

- [Manual billing does not scale] -> Limit it to pilots and preserve a provider adapter boundary.
- [Provider state and local access diverge] -> Reconcile periodically and derive transitions from verified events with idempotency keys.
- [Commercial configuration bypasses authorization] -> Intersect entitlement with template availability and actor permission server-side.
- [Payment provider is unavailable in the target market] -> Delay selection until commercial/legal validation and support manual collection.

## Migration Plan

1. Establish the capability registry with identity and templates.
2. Add plan/subscription models and manual activation.
3. Validate pilot pricing and operational limits.
4. Research and select a provider through a separate decision record.
5. Add provider adapter, verified webhook lifecycle, reconciliation, and recovery tests.

Rollback freezes subscription mutations and restores the previous entitlement snapshot. Tenant data is retained and remains exportable during commercial suspension.

## Research Evidence

- Stripe Billing official documentation and web research, accessed 2026-09-27: explicit subscription states, invoices, payment retries, webhooks, and entitlements tied to active products.
- Wompi Colombia official documentation, accessed 2026-09-27: tokenized payment sources can support future monthly charges, private-key operations must remain server-side, and sensitive payment information must not be stored by the merchant.
- Independent architecture verification, 2026-09-27: entitlement must intersect module availability and actor authorization; webhook persistence should wait for provider selection.

## Open Questions

- Will pilot customers pay monthly by transfer/invoice or require automatic collection?
- Which Colombian tax and electronic-invoicing obligations apply to the seller?
- What grace period and read-only behavior are commercially acceptable?