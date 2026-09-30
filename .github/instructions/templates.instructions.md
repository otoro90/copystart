---
name: 'Vertical templates'
description: 'Use when adding vertical-specific behavior, template configuration, labels, catalogs, or workflow options for a tenant type.'
applyTo: '{src,web}/**/*{Template,Vertical}*'
---
# Template Boundary

- Templates configure only registered surfaces: vocabulary, navigation order, catalogs, forms, theme tokens, header/footer content, and module-defined workflow options.
- New entities, invariants, validations, permissions, integrations, or side effects require a typed, reusable module. Expose only its options through the template.
- Prefer generic capabilities over vertical-specific code (for example, a meter-reading capability instead of "odometer for cars").
- Template versions are immutable. Tenants pin a version; upgrades are previewed and never overwrite overrides silently.
- Templates never contain scripts, SQL, expressions, or arbitrary HTML/CSS.
- Never add per-tenant `if` branches.
