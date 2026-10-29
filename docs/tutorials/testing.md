# Testing Guide

| Level | Scope | Tooling |
| --- | --- | --- |
| Unit | Domain invariants and application handlers | xUnit, no I/O |
| Integration | EF Core, PostgreSQL, RLS, SeaweedFS | xUnit + real services in isolated databases/buckets |
| Tenant isolation | Every tenant-owned query, cache, file, and job | Two-tenant fixtures; forged headers and IDs |
| API contract | Versioned endpoints and Problem Details | Integration host + OpenAPI checks |
| Frontend | Components, guards, services | Angular test runner |
| E2E / accessibility | Journeys, keyboard, screen reader, zoom | Playwright, axe |

Write the failing test first. Never weaken an assertion to make a test pass.
