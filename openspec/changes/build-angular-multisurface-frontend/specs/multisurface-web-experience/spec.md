## ADDED Requirements

### Requirement: Public and operational experiences deploy independently
The frontend SHALL use one Angular workspace containing an independently deployable SSR/SSG public site and a client-rendered operations application.

#### Scenario: Operations release is delayed
- **WHEN** a staff-console change is not ready for production
- **THEN** the public site can be built and released without that change

### Requirement: Public rendering is tenant-correct
The public application MUST resolve the tenant from the request host during server rendering and emit tenant-correct content, metadata, canonical URL, theme, header, and footer before hydration.

#### Scenario: Search crawler requests a tenant homepage
- **WHEN** JavaScript execution is unavailable
- **THEN** the returned HTML identifies the correct tenant and contains its public primary content and metadata

### Requirement: Operational routes are audience-separated
The operations application SHALL provide lazy customer, staff, and platform-administration route areas with explicit authorization metadata and distinct navigation.

#### Scenario: Customer signs in
- **WHEN** the user has customer permissions only
- **THEN** customer routes load and staff or platform features are absent and API-protected

### Requirement: Modern Angular conventions are enforced
New frontend code MUST use Angular 22 standalone components, Signals for local reactive state, native control flow, `inject()`, strict TypeScript, reactive forms, OnPush change detection, and typed API clients unless an approved ADR documents an exception.

#### Scenario: Frontend static validation runs
- **WHEN** a component violates the approved conventions
- **THEN** lint, type checking, architecture tests, or review gates fail before merge

### Requirement: Accessibility and performance are release gates
Both applications MUST meet WCAG 2.2 AA acceptance scenarios and explicit compressed initial/lazy bundle, Core Web Vitals, keyboard, screen-reader, zoom, reduced-motion, responsive, and low-bandwidth budgets.

#### Scenario: A tablet technician completes a work order
- **WHEN** the workflow is tested at supported tablet sizes with keyboard and touch
- **THEN** controls remain reachable, labels remain visible, state is preserved, and no horizontal overflow blocks completion