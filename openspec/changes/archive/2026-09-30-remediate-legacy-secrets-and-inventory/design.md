## Context

The historical development configuration contains a PostgreSQL credential and the repository tracks one PNG beneath the prototype upload directory. No authoritative database dump or external upload-store snapshot has been confirmed. Migration work must not normalize this uncertainty by assuming all live cluster data belongs to the prototype.

## Goals / Non-Goals

**Goals:**
- Remove the legacy development credential from tracked configuration and establish whether it remains active or was reused.
- Record a privacy-preserving checksum inventory of the tracked PNG and any database/file snapshot explicitly nominated by its owner.
- Provide evidence to decide whether a later import is applicable, without creating an importer or a generic data-discovery system.

**Non-Goals:**
- Rotate mini-cluster/OpenBao secrets or modify SeaweedFS configuration.
- Create CI workflows or a continuous repository secret-scanning service.
- Discover or inventory unapproved databases, cluster buckets, or users' personal files.
- Automatically rewrite Git history.
- Transform or migrate production data.
- Select the target domain schema.
- Delete the historical database, files, or Git history.

## Decisions

1. Remove the credential from tracked prototype config immediately and replace it with an inert example. Ask the authorized database owner whether the old value remains accepted or was reused; rotate only active/reused credentials and verify rejection without recording the replacement.
2. Assess Git history for exposure and report redacted paths/commit identifiers. History rewriting is not automatic; first revoke active credentials and separately evaluate operational consequences.
3. Hash the tracked PNG in streaming mode and record path, size, media type, and SHA-256. Inventory database rows/schema or external files only when an owner identifies an accessible, immutable source snapshot and authorizes read-only inspection.
4. If no authoritative data source is found, record database/file migration inventory as not applicable. Backend scaffolding, domain work, and dependency remediation do not wait for this conditional migration gate.
5. This is a protected Day-0 repository/operator activity, not a cluster bootstrap stage. Mini-cluster/OpenBao credentials are owned by the mini-cluster changes; continuous secret scanning is owned by `establish-github-ci-and-image-publishing`.

## Risks / Trade-offs

- [Credential has been reused outside the prototype] -> Ask the authorized owner to identify consumers and rotate only affected active uses; never print replacement values.
- [Inventory exposes personal information] -> Keep row-level content out of the report; store any restricted source snapshot/report outside Git with owner-approved access.
- [No authoritative database or file snapshot exists] -> Mark the import inventory not applicable and keep the new service independent of prototype data.

## Migration Plan

1. Replace tracked prototype credentials with inert placeholders and identify the authorized owner of any backing service.
2. Verify whether each value is active/reused; if so, rotate it out of band and confirm the old value is rejected.
3. Assess Git history and document redacted evidence; perform no history rewrite under this change.
4. Hash the tracked PNG. Inventory an owner-provided immutable database or file snapshot only if one is confirmed and authorized.
5. Run any applicable read-only inventory twice and compare normalized output; otherwise record the source as unavailable/not applicable.

Rollback restores application configuration to the new credential reference. Compromised credentials are never reactivated.

## Research Evidence

- Local repository inspection, 2026-09-27: tracked development configuration contains PostgreSQL connection values; one PNG is tracked under `Application/CopyStart/filesystem/archivos/`; no tracked SQL dump was found.
- Decision verification, 2026-09-27: credential revocation is conditional on current use, and data import is conditional on finding an authoritative source; neither blocks backend scaffolding or dependency audit.
- Repository owner confirmation, 2026-09-30: the prototype has been stopped for years, was never deployed to production, and its PostgreSQL credential was not reused. No authoritative database or external-upload snapshot exists. The credential is retired; rotation is not applicable, and its historical value remains compromised and must never be reused.
- Local inventory, 2026-09-30: `Application/CopyStart/filesystem/archivos/2ab1735c-b83c-4e41-af4a-c1dcd0af9c07` is a 761,227-byte PNG with SHA-256 `0fb86c2c1d52b9486b8e374c8c665cad3ca34eceae93052e699dcca1db291ea7`. This is one tracked file, not a complete upload corpus.
- Git history assessment, 2026-09-30: credential-bearing configuration paths were touched in six commits: `5a59fd7` and `2883435` (2024-01-06), `3d93d15` (2022-10-21), `b3594d2` (2022-10-19), `e2fc0a6` (2022-10-05), and `d37e8ac` (2022-10-04). History was not rewritten.
- Configuration remediation, 2026-09-30: tracked JSON and Compose defaults no longer contain a live credential. Local development uses .NET User Secrets or ignored `Application/.env`; Docker excludes local env files from its build context.

## Open Questions

- Resolved: the owner confirms the historical credential is retired and was not reused; rotation is not applicable.
- Resolved: no authoritative source database or external-upload snapshot is available; migration inventory is not applicable.