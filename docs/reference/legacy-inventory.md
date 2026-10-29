# Legacy Prototype Security Inventory

Verified: 2026-09-30.

## Source Decision

The repository owner confirms that the CopyStart prototype has been stopped for years, was never deployed to production, and its PostgreSQL credential was not reused. The credential is retired; rotation is not applicable. Its historical value remains compromised and must never be reactivated or reused.

No authoritative database dump or external-upload snapshot is available or approved. There is no tenant-owned SaaS data in scope. Database and external-file migration inventory is therefore not applicable; the new SaaS starts without importing prototype records. No general importer or data-discovery tool was created.

## Tracked Upload Evidence

| Path | Bytes | Media type | SHA-256 |
| --- | ---: | --- | --- |
| `Application/CopyStart/filesystem/archivos/2ab1735c-b83c-4e41-af4a-c1dcd0af9c07` | 761227 | `image/png` | `0fb86c2c1d52b9486b8e374c8c665cad3ca34eceae93052e699dcca1db291ea7` |

This record covers one tracked file only and does not claim to represent the complete historical upload corpus.

Recheck this explicitly selected file without rendering or modifying it:

```sh
file Application/CopyStart/filesystem/archivos/2ab1735c-b83c-4e41-af4a-c1dcd0af9c07
stat -f 'bytes=%z' Application/CopyStart/filesystem/archivos/2ab1735c-b83c-4e41-af4a-c1dcd0af9c07
shasum -a 256 Application/CopyStart/filesystem/archivos/2ab1735c-b83c-4e41-af4a-c1dcd0af9c07
```

The normalized output was checked twice against the same tracked path and matched. The source hash was unchanged. The command reads only the explicitly named file; it does not query a database, enumerate buckets, or export file contents.

## Git History

Credential-bearing configuration history remains in the repository and was not rewritten. Redacted commit/date evidence for the tracked configuration paths:

| Commit | Date |
| --- | --- |
| `5a59fd7` | 2024-01-06 |
| `2883435` | 2024-01-06 |
| `3d93d15` | 2022-10-21 |
| `b3594d2` | 2022-10-19 |
| `e2fc0a6` | 2022-10-05 |
| `d37e8ac` | 2022-10-04 |

Affected paths include `Application/CopyStart/appsettings.json`, `Application/CopyStart/appsettings.Development.json`, and `Application/docker-compose.yml`. The current tracked files no longer contain the credential. A history purge requires a separate decision after evaluating clone, fork, and operational impact.

## Local Configuration

- Development uses the existing .NET User Secrets support for `ConnectionStrings:DefaultConnection`.
- Docker Compose requires `COPYSTART_DB_CONNECTION` and `COPYSTART_DB_PASSWORD` from the ignored `Application/.env` file. `Application/.env.example` contains inert replacement values only.
- `.gitignore` excludes `Application/.env`; `Application/.dockerignore` excludes local env files from the build context.

No production credential, database access, external snapshot, or secret value is recorded here.