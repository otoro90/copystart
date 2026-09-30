# ADR-0001: Angular 22 over React

- Status: Accepted, conditional on the frontend spike.
- Date: 2026-09-27.

## Decision

Use one Angular 22 workspace with two deployables: `public-site` (SSR/SSG) and `operations` (client-rendered customer, staff, and platform areas).

## Rationale

- Angular provides first-party routing, forms, DI, Signals, SSR/SSG, hydration, and `ng update` migrations. v22 is active until June 2027 and LTS until June 2028.
- React's official guidance recommends adding a framework (Next.js, React Router) for production, adding a second architecture decision without shown benefit here.
- GovCore uses Angular 22.1 and supplies team experience and test scenarios. Its production components are not reused.

## Acceptance Checks

Tenant-correct SSR HTML before JavaScript, hydration without mismatch, passing compressed bundle budgets, OIDC PKCE without token logging, WCAG 2.2 AA checks, and independent deployment of both apps.

## Reevaluation Triggers

Repeated missed public performance budgets, SEO needs better served elsewhere, a native/offline-first requirement, or a time-boxed React/Next.js spike that clearly outperforms Angular.

## Sources

- https://angular.dev/reference/releases (accessed 2026-09-27)
- https://react.dev/learn/creating-a-react-app (accessed 2026-09-27)
