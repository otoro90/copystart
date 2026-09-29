## Why

The current Razor/AdminLTE interface is static, tightly coupled to Copy Start branding, and mixes public, customer, and staff concerns. A modern client must support operational forms, tenant branding, responsive tablet workflows, and independently understandable user surfaces.

## What Changes

- **BREAKING** Replace server-rendered MVC views with an Angular 22 application consuming the versioned API.
- Use one Angular workspace with two deployables: an SSR/SSG public site and a client-rendered operations application containing customer, staff, and platform-administration route areas.
- Use standalone components, Signals, native control flow, lazy routes, reactive forms, strict typing, and accessible responsive interaction patterns.
- Resolve public tenant context during server rendering and add a tenant-resolution gate so no branded shell hydrates or renders before tenant configuration is confirmed.
- Establish a neutral design system and design tokens without importing GOV.CO visual identity.
- Record Angular-over-React as an architecture decision with explicit reevaluation triggers.

## Capabilities

### New Capabilities
- `multisurface-web-experience`: Provides the public, customer, staff, and platform web experiences with shared accessible components and tenant-aware shells.

### Modified Capabilities

None.

## Impact

Adds an Angular 22 workspace with independently deployable public and operations applications, then retires Razor/AdminLTE as features are migrated. Reuses GovCore test cases and engineering lessons, not its production components, governmental terminology, or token-logging behavior. Depends on `establish-project-governance` and the API contracts from `modernize-service-platform-backend`.