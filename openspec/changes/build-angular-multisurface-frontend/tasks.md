## 1. Framework Spike

- [ ] 1.1 Define measurable SSR, hydration, bundle, accessibility, and tablet-workflow acceptance budgets
- [ ] 1.2 Build a disposable Angular 22 spike for tenant SSR, OIDC PKCE, one work-order form, and lazy routing
- [ ] 1.3 Measure the spike and record whether any reevaluation trigger requires a React/Next.js comparison

## 2. Workspace and Shared Libraries

- [ ] 2.1 Create the Angular workspace with independently deployable `public-site` and `operations` applications
- [ ] 2.2 Add strict configuration, linting, unit tests, accessibility checks, and compressed bundle budgets
- [ ] 2.3 Build neutral design-token, UI-primitive, API-contract, tenant-context, and authentication libraries
- [ ] 2.4 Generate or implement typed API clients without leaking transport models into presentation components

## 3. Application Shells

- [ ] 3.1 Implement tenant-correct public SSR/SSG, metadata, canonical URLs, header, footer, and hydration
- [ ] 3.2 Implement the operations bootstrap gate and lazy customer, staff, and platform route areas
- [ ] 3.3 Implement loading, empty, error, unauthorized, offline/degraded, and success states
- [ ] 3.4 Migrate one request-to-work-order vertical slice and verify API authorization independently of route guards

## 4. Verification and Cutover

- [ ] 4.1 Run unit, integration, Playwright, keyboard, screen-reader, zoom, reduced-motion, responsive, and low-bandwidth checks
- [ ] 4.2 Verify tenant-correct HTML before JavaScript and hydration without mismatch
- [ ] 4.3 Verify both applications build and deploy independently within performance budgets
- [ ] 4.4 Document incremental Razor retirement and run strict OpenSpec validation