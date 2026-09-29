## ADDED Requirements

### Requirement: Commercial access is provider-neutral
The platform SHALL represent plans, plan versions, prices, billing accounts, subscriptions, trials, grace periods, cancellations, and entitlements independently of any payment provider.

#### Scenario: Pilot tenant is invoiced manually
- **WHEN** a platform operator activates an approved manual subscription
- **THEN** the tenant receives the plan's entitlements without requiring a payment-provider customer ID

### Requirement: Effective capabilities are intersected
The API MUST grant a capability only when its module is available to the tenant template, its subscription entitles it, and the actor is authorized.

#### Scenario: Plan includes inventory but template disables it
- **WHEN** a user requests an inventory operation
- **THEN** access is denied and the client receives an unavailable capability state

### Requirement: Entitlements are enforced server-side
Every entitled operation and usage limit MUST be enforced by the API; client-side feature visibility SHALL be informational only.

#### Scenario: Client modifies local entitlement state
- **WHEN** it calls a feature excluded from the tenant's effective capabilities
- **THEN** the API rejects the operation without changing data

### Requirement: Subscription transitions are auditable and idempotent
Subscription state changes MUST record source, external reference when present, effective time, actor/event identity, and idempotency key.

#### Scenario: Provider event is delivered twice
- **WHEN** the same verified webhook is processed repeatedly
- **THEN** one logical transition occurs and duplicate deliveries are retained as audit evidence

### Requirement: Payment data remains external
The application MUST NOT store raw card, bank-account, wallet credentials, provider private keys, or reusable secrets in tenant data.

#### Scenario: Payment method is enrolled
- **WHEN** a provider returns a token or payment-source identifier
- **THEN** only the minimum provider reference and non-sensitive display metadata are retained