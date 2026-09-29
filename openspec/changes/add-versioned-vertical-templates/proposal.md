## Why

Small service businesses share operational concepts but use different vocabulary, catalogs, priorities, forms, and workflows. Per-customer code forks would make the SaaS uneconomic, while unrestricted configuration would create an unsafe low-code platform.

## What Changes

- Introduce immutable, versioned vertical templates with tenant-pinned versions and explicit upgrade previews.
- Configure vocabulary, navigation order, enabled modules, seeded catalogs, theme tokens, header/footer content, forms, and bounded workflow profiles.
- Support tenant overrides with provenance, validation, locked fields, rollback, and conflict reporting.
- Ship initial copier-service and automotive-workshop templates to test the shared-domain boundary.
- Require a typed vertical module when a variation introduces new entities, invariants, permissions, resources, integrations, or side effects.

## Capabilities

### New Capabilities
- `vertical-template-management`: Provides versioned vertical defaults, tenant overrides, branding, vocabulary, navigation, catalogs, and bounded workflow configuration.

### Modified Capabilities

None.

## Impact

Adds template/configuration persistence, resolution APIs, administration UX, and two reference templates. Depends on `modernize-service-platform-backend`, `build-angular-multisurface-frontend`, and `add-multitenancy-and-zitadel-identity`.