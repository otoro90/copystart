---
name: 'Frontend Angular'
description: 'Use when creating or changing Angular components, routes, services, forms, or styles in the public-site or operations apps.'
applyTo: 'web/**/*.{ts,html,scss}'
---
# Frontend Conventions

- Angular 22 standalone components, `ChangeDetectionStrategy.OnPush`, `inject()`, Signals for local state, and native control flow (`@if`, `@for`, `@switch`, `@defer`).
- Strict TypeScript. No `any`; use `unknown` and narrow.
- Reactive forms only.
- `public-site` renders tenant content on the server; never read `window` during SSR.
- `operations` renders no tenant-branded shell until tenant configuration resolves.
- Route guards are UX only. The API enforces authorization.
- Never log access tokens, ID tokens, or presigned URLs.
- Style through semantic design tokens. Tenant data selects token values; it never injects CSS, HTML, or scripts.
- Every view handles loading, empty, error, unauthorized, and success states and meets WCAG 2.2 AA.
