## Context

CopyStart uses Razor views and AdminLTE with static branding. GovCore currently uses Angular 22.1 and provides useful examples for Signals, tenant configuration, OIDC, and failure-state tests, but it also contains mixed NgModule/standalone patterns, token logging, `any`, default change detection, and no demonstrated SSR setup. The target must reuse knowledge, not source code.

## Goals / Non-Goals

**Goals:**
- Deliver accessible public and operational experiences with independent release characteristics.
- Reuse a neutral design system, typed API contracts, tenant configuration, and authentication abstractions.
- Support public discoverability and efficient tablet workflows.

**Non-Goals:**
- Import GOV.CO visual identity or components.
- Treat route guards as security enforcement.
- Build native mobile applications in the first release.

## Decisions

1. Choose Angular 22. Official documentation provides first-party DI, routing, forms, Signals, SSR/SSG, hydration, accessibility guidance, and update tooling. GovCore supplies relevant team experience and test scenarios.
2. Reject React for the initial implementation. React is capable, but its official guidance recommends selecting another framework for production concerns; that adds a second architecture choice without a demonstrated product advantage here.
3. Use one workspace with `public-site` and `operations` applications plus libraries for design tokens, UI primitives, API contracts, tenant context, and auth integration. Avoid a single deployment because SEO/caching and authenticated release risks differ.
4. Resolve tenant context server-side for public rendering and transfer the exact resolved configuration into hydration. The operations app uses a blocking bootstrap gate before rendering tenant UI.
5. Define neutral semantic tokens and bounded component variants. Tenant data selects values; it does not inject arbitrary CSS, scripts, or component markup.
6. Use generated typed clients from OpenAPI where practical. Keep remote state close to features; add NgRx only for genuinely shared event-driven state.
7. Require a representative spike before broad implementation: tenant SSR/hydration, OIDC PKCE, one work-order form, route lazy loading, and measured accessibility/performance.

## Risks / Trade-offs

- [Angular bundle size harms public performance] -> Separate deployables, SSR/SSG, route-level deferral, explicit budgets, and measured Core Web Vitals.
- [Hydration resolves a different tenant] -> Resolve once from trusted host data and transfer immutable bootstrap state.
- [GovCore defects are copied] -> Reimplement from requirements and tests; prohibit token logging and untyped service contracts.
- [Two deployables duplicate UI] -> Share only stable libraries and keep shell composition application-specific.

## Migration Plan

1. Build the representative Angular spike and compare it against acceptance budgets.
2. Create workspace libraries and both application shells.
3. Implement public tenant SSR and operational bootstrap/auth.
4. Migrate one vertical slice end to end before expanding routes.
5. Replace Razor routes incrementally and remove AdminLTE only after parity checks.

Rollback routes affected paths to the preserved MVC application. The two Angular deployables can roll back independently.

## Research Evidence

- Angular official documentation and Context7, accessed 2026-09-27: Angular 22 provides integrated routing, forms, DI, Signals, SSR/SSG, hydration, security, accessibility, and migration tooling; v22 active support runs through June 2027 and LTS through June 2028.
- React official documentation and Context7, accessed 2026-09-27: React recommends a framework such as Next.js or React Router for production applications; starting from scratch requires selecting routing and data-fetching solutions.
- Local GovCore `package.json` and tenant/auth services, inspected 2026-09-27: Angular 22.1 and OIDC 22 are in active use.
- Independent frontend verification, 2026-09-27: Angular is conditionally accepted; two deployables and an SSR/performance/accessibility spike are required.

## Open Questions

- What measurable public LCP and compressed JavaScript budgets fit the target customers' connectivity?
- Does the first pilot require offline drafts or only resilient retry?
- Which neutral component library, if any, meets branding and accessibility needs without excessive payload?