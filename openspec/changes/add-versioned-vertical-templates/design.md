## Context

The historical model uses general entities but embeds copier terminology in data annotations and public views. Exploration found that vocabulary and display order can be configured, while structural differences such as vehicle mileage or copier meter readings require typed modules. The design must avoid both tenant forks and an unrestricted low-code engine.

## Goals / Non-Goals

**Goals:**
- Adapt one product to multiple service-business verticals.
- Preserve canonical domain semantics and testable invariants.
- Allow safe tenant customization and controlled template evolution.

**Non-Goals:**
- Execute customer-provided code, SQL, CSS, or arbitrary HTML.
- Guarantee that every possible workshop fits one domain model.
- Auto-upgrade tenant configurations without review.

## Decisions

1. Separate `TemplateDefinition`, immutable `TemplateVersion`, `TenantTemplateAssignment`, and field-level `TenantOverride` with provenance.
2. Treat templates as curated product assets stored as validated structured documents and seeded through reviewed releases. Runtime values are materialized/cached for efficient reads.
3. Maintain a canonical capability registry shared with subscriptions. Effective feature availability is `module available by template` AND `commercially entitled` AND `actor authorized`.
4. Allow bounded workflow profiles to choose module-defined states, transitions, forms, notifications, and required fields. New invariants or external effects require code in a typed module.
5. Restrict visual customization to semantic tokens, approved fonts/assets, content slots, menu definitions, and accessible component variants.
6. Resolve configuration in layers: platform defaults -> template version -> tenant overrides. Every resolved field exposes provenance to administration tools.
7. Start with copier-service and automotive-workshop templates as a deliberate generality test, not as promises to support all repair industries.

## Risks / Trade-offs

- [Configuration becomes a second programming language] -> Maintain allowlisted schemas and module-owned behavior; prohibit expressions and scripts.
- [Template upgrades break active work] -> Pin versions, preview diffs, migrate explicitly, and preserve rollback snapshots.
- [Terminology hides semantic mismatch] -> Require domain review when a label change accompanies different lifecycle or invariants.
- [Too many overrides destroy product consistency] -> Lock security, audit, and core workflow fields and measure override usage.

## Migration Plan

1. Define the capability registry and template schema.
2. Encode the copier-service template from validated legacy behavior.
3. Build configuration resolution and provenance tests.
4. Add automotive modules only where canonical concepts are insufficient.
5. Implement preview, apply, rollback, and administration UX.
6. Validate both reference scenarios and document unsupported variations.

Rollback reassigns the tenant to its prior immutable template version and override snapshot. Work records retain the workflow/version metadata under which they were created.

## Research Evidence

- Local CopyStart entities and controllers, inspected 2026-09-27: customer, request, service, asset, procedure, and part concepts are shared, while labels and public content are copier-specific.
- Local GovCore header/footer and tenant state, inspected 2026-09-27: runtime tenant configuration is feasible but governmental field shapes must not be copied.
- Independent architecture verification, 2026-09-27: labels are insufficient when a vertical introduces new entities, invariants, resources, permissions, integrations, or side effects.

## Open Questions

- Which workflow differences are confirmed by the automotive pilot rather than assumed?
- Which template fields may a tenant administrator change without platform review?
- How long must historical template versions remain renderable?