# Frontend Acceptance Budgets — CopyStart SaaS

Last updated: 2026-10-02.
Governing change: `build-angular-multisurface-frontend` (Gate 9).
Architecture Decision: [ADR-0001: Angular 22 over React](decisions/0001-angular-over-react.md).

---

## 1. Context & Operational Environments

CopyStart delivers two distinct web experiences from a single Angular 22 workspace:
1. **`public-site` (SSR / SSG)**: Public marketing, discovery, tenant landing pages, service catalog, and customer request intake. Must be lean, crawlable, and fast on mobile/cellular networks.
2. **`operations` (Client-Rendered SPA)**: Authenticated workspaces for Customers, Technicians/Staff (in-workshop and on-site repair tablets), and Platform Admins. Optimized for rapid data entry, reactive updates via Signals, and ergonomic touch interaction.

---

## 2. Measurable Performance & Web Vitals Budgets

| Metric | Target (`public-site` SSR) | Target (`operations` SPA) | Measurement Condition |
| :--- | :--- | :--- | :--- |
| **LCP (Largest Contentful Paint)** | ≤ 2.0 s (P75) | ≤ 2.5 s (P75 post-auth) | Emulated mobile (Slow 4G, 4x CPU throttle) |
| **INP (Interaction to Next Paint)** | ≤ 100 ms | ≤ 150 ms | Active form entry & tab switching |
| **CLS (Cumulative Layout Shift)** | ≤ 0.05 | ≤ 0.05 | Zero layout jumps during hydration or dynamic card loads |
| **TTFB (Time to First Byte)** | ≤ 500 ms | N/A (static assets cached) | Traefik reverse proxy to SSR Node engine |
| **FCP (First Contentful Paint)** | ≤ 1.2 s | ≤ 1.5 s | Initial HTML render |

---

## 3. Hydration & Server-Side Rendering Budgets

1. **Zero Hydration Mismatch**:
   - The SSR engine renders the exact tenant theme, metadata, canonical URL, header, and primary content matching the incoming `Host` header.
   - Hydration on the client must complete without any Angular hydration errors (`NG0500`, `NG0501`, `NG0502`).
2. **HTML Completeness Without JavaScript**:
   - A search crawler or low-bandwidth browser with JavaScript disabled must receive full semantic HTML containing:
     - Page title `<title>` and `<meta name="description">`
     - Canonical `<link rel="canonical">` pointing to the tenant host
     - Tenant name, contact details, and public service catalog
     - Accessible request intake instructions
3. **Selective Deferral**:
   - Interactive widgets, heavy modal dialogs, and non-critical assets must use native Angular `@defer (on viewport)` or `@defer (on idle)` to avoid blocking initial hydration.

---

## 4. Compressed Bundle Size Budgets

Enforced via `angular.json` build budgets (producing build warnings at 80% and build failures at 100%):

| Target Artifact | Max Transfer Size (Brotli / Gzip) | Max Raw Uncompressed | Rationale |
| :--- | :--- | :--- | :--- |
| **`public-site` Initial Bundle** | **≤ 45 kB** (Transfer) | ≤ 140 kB (Raw) | Instant first paint on cellular connections |
| **`public-site` Stylesheet** | **≤ 15 kB** (Transfer) | ≤ 50 kB (Raw) | Critical CSS inlined by Angular SSR |
| **`operations` Initial Bundle** | **≤ 110 kB** (Transfer) | ≤ 380 kB (Raw) | Core SPA runtime, Signals state, and Auth gate |
| **`operations` Lazy Route Chunks** | **≤ 35 kB** (Transfer) | ≤ 120 kB (Raw) | Each feature module (`orders`, `assets`, `billing`) loads on demand |
| **Vendor / Polyfills** | **≤ 20 kB** (Transfer) | ≤ 65 kB (Raw) | Modern browsers (ES2024 target, evergreen only) |

---

## 5. Accessibility (WCAG 2.2 AA) Budgets

Both applications must pass automated and manual accessibility audits:

| Criterion | Standard | Implementation Requirement |
| :--- | :--- | :--- |
| **Color Contrast (Text)** | WCAG 2.2 SC 1.4.3 | Minimum **4.5:1** for regular text; **3:1** for large text (≥ 18pt or bold 14pt). |
| **Non-text Contrast** | WCAG 2.2 SC 1.4.11 | Minimum **3:1** for form control borders, icons, and focus indicators against adjacent colors. |
| **Focus Not Obscured** | WCAG 2.2 SC 2.4.11 / 2.4.12 | Focused elements must have a visible outline (`2px solid var(--color-focus, #005fb8)`) with ≥ 2px offset; never obscured by sticky headers or bottom action bars. |
| **Target Size (Minimum)** | WCAG 2.2 SC 2.5.8 | Minimum interactive touch target of **24 × 24 CSS px**, with target **44 × 44 CSS px** for primary workshop actions. |
| **Redundant Entry** | WCAG 2.2 SC 3.3.7 | Previously entered customer, asset, or tenant info must be auto-populated; never re-prompted within the same workflow. |
| **Keyboard Accessibility** | WCAG 2.2 SC 2.1.1 | 100% operable via `Tab`, `Shift+Tab`, `Enter`, `Space`, `Escape`, and arrow keys; no keyboard traps. |
| **Reduced Motion** | WCAG 2.2 SC 2.3.3 | All transitions and animations respect `@media (prefers-reduced-motion: reduce)`. |

---

## 6. Workshop Tablet & Field Ergonomics (Technician Usability)

Tested against standard technician devices (e.g., Apple iPad 10.2", Samsung Galaxy Tab Active 8.0"):

1. **Touch Ergonomics**:
   - Form inputs, buttons, and status toggles have minimum height of `48px` with generous tap padding (`12px 16px`).
   - Radio cards and checklist items for inspection procedures must use full-width tap surfaces.
2. **Zero Horizontal Overflow**:
   - Responsive breakpoints: `320px` (mobile), `768px` (tablet portrait), `1024px` (tablet landscape / desktop).
   - Data tables on tablet screens must degrade gracefully into stacked card views or provide sticky header columns.
3. **Resilience to Transient Drops**:
   - Work order notes and checklist inputs must persist in browser storage (`IndexedDB` / `localStorage`) locally until confirmed saved by the backend API, preventing data loss if workshop Wi-Fi blips.

---

## 7. Reevaluation Triggers Against React/Next.js (ADR-0001)

If the Angular 22 spike or implementation experiences any of the following, a time-boxed React/Next.js comparative evaluation is triggered:
- Public initial compressed transfer bundle exceeds **60 kB** despite SSR and lazy loading.
- Angular SSR cold-start latency exceeds **800 ms** in containerized ARM64 testing on K3s.
- Hydration mismatches cannot be eliminated without abandoning SSR for public content.
- Offline synchronization complexity requires native bindings better supported in alternate ecosystems.
