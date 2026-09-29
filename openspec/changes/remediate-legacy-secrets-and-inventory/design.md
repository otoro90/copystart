## Context

`Application/CopyStart/appsettings.json` and `Application/docker-compose.yml` contain tracked PostgreSQL credentials. The application also stores uploads under `PathBaseFiles`, but there is no authoritative inventory of the database or filesystem. Migration work must not normalize this risk by copying it into a new architecture.

## Goals / Non-Goals

**Goals:**
- Rotate exposed credentials and remove live values from tracked content.
- Establish a deterministic, read-only inventory and migration baseline.
- Prevent future commits containing secrets.

**Non-Goals:**
- Transform or migrate production data.
- Select the target domain schema.
- Delete the historical database, files, or Git history.

## Decisions

1. Treat every tracked credential as compromised. Rotation happens through an authorized operator; reports record identifiers and timestamps, never replacement values.
2. Inventory tools run with read-only database permissions and hash files in streaming mode. Outputs contain counts and digests, not sensitive row contents.
3. Preserve an immutable source snapshot before any later migration. Destructive history rewriting requires a separate explicit decision because it affects every clone.
4. Add secret scanning to local validation and CI with redacted findings and an allowlist review process.
5. This is a protected Day-0/operator activity, not an automatic `bootstrap-full-cluster.sh` stage. New application secrets are handled by `provision-saas-platform-prerequisites`.

## Risks / Trade-offs

- [Credentials remain usable in an unknown environment] -> Enumerate owners and verify revocation with the backing service.
- [Inventory exposes personal information] -> Record structural statistics and hashes; encrypt restricted reports and exclude them from Git.
- [Rotation interrupts the legacy prototype] -> Confirm consumers, rotate in a controlled window, and retain a tested new-secret rollback path without restoring the compromised secret.

## Migration Plan

1. Discover and classify tracked secrets without echoing values.
2. Rotate each credential and verify old-value rejection.
3. Replace tracked values with placeholders or secret references.
4. Create and checksum database and file snapshots.
5. Run the read-only inventory twice and compare normalized output.
6. Enable secret scanning and verify a synthetic credential is rejected.

Rollback restores application configuration to the new credential reference. Compromised credentials are never reactivated.

## Research Evidence

- Local repository inspection, 2026-09-27: tracked credentials in `Application/CopyStart/appsettings.json` and `Application/docker-compose.yml`; direct filesystem storage in `Areas/Soportes/Controllers/ArchivosController.cs`.
- Decision verification, 2026-09-27: classified credential rotation as the first prerequisite for all migration work.

## Open Questions

- Which environments still accept the historical credentials?
- Where are the authoritative database and file snapshots currently held?