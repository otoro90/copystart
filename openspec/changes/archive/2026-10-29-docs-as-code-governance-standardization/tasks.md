# Tasks: Docs-as-Code Governance Standardization in CopyStart

## 1. Scaffolding de Taxonomía Diátaxis y Gobernanza ADR

- [x] 1.1 Crear la estructura de directorios Diátaxis en `/docs/` (`tutorials/`, `runbooks/`, `architecture/`, `reference/`) y el repositorio `docs/adr/`. Verificar con `ls -la docs/`.
- [x] 1.2 Crear la plantilla base MADR v4 y el índice `docs/adr/README.md`. Verificar formato.

## 2. Redacción de ADRs Fundacionales (MADR v4)

- [x] 2.1 Redactar `docs/adr/0001-retiro-prototipo-net6-y-descarte-migracion-datos.md` documentando el fin de soporte de .NET 6 y la decisión de iniciar el nuevo SaaS en limpio.
- [x] 2.2 Redactar `docs/adr/0002-multi-tenancy-por-plantillas-verticales.md` documentando el aislamiento por empresa y personalización de flujos para fotocopiadoras y talleres.
- [x] 2.3 Redactar `docs/adr/0003-identificadores-uuidv7-para-ordenes-y-activos.md` documentando la generación nativa en memoria con `Guid.CreateVersion7()`.
- [x] 2.4 Redactar `docs/adr/0004-pure-handlers-wolverinefx-y-transaccionalidad-automatica.md` documentando la prohibición de SaveChanges en handlers.
- [x] 2.5 Redactar `docs/adr/0005-stack-tecnologico-canonico-dotnet10-angular22.md` documentando el stack .NET 10 LTS, Angular 22 Signals y PostgreSQL 17.

## 3. Modelo C4 en Mermaid Flowchart con classDef

- [x] 3.1 Redactar `docs/architecture/c4-model.md` con los diagramas C4 (Contexto L1, Contenedores L2 y Componentes L3) utilizando sintaxis Mermaid Flowchart y `classDef` estándar.

## 4. Reorganización Diátaxis y Gobernanza AGENTS.md

- [x] 4.1 Reclasificar y migrar documentos existentes a los cuadrantes Diátaxis correspondientes y actualizar `docs/README.md` como mapa maestro de navegación.
- [x] 4.2 Actualizar `AGENTS.md` incorporando la prohibición de archivos de lecciones aprendidas y directivas Docs-as-Code.
- [x] 4.3 Validar el cambio completo con `openspec validate docs-as-code-governance-standardization --strict`.

## Workflow follow-up
- Archivar el cambio con `openspec archive docs-as-code-governance-standardization --yes`.
