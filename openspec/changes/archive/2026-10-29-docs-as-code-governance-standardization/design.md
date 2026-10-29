# Design: Docs-as-Code Governance Standardization in CopyStart

## Context
Ver `proposal.md` para motivación y justificación general. CopyStart implementa un SaaS multi-tenant para empresas de servicio técnico, talleres mecánicos y mantenimiento de activos. El repositorio incluye histórico de un prototipo en .NET 6 MVC y la especificación de la nueva plataforma en .NET 10 y Angular 22.

El diseño establece la taxonomía Diátaxis, formaliza los primeros 5 ADRs en MADR v4 y unifica los diagramas C4 en Mermaid Flowchart con clases `classDef` estables.

## Goals / Non-Goals

**Goals:**
- Estructurar `/docs/` en `tutorials/`, `runbooks/`, `architecture/`, `reference/` y `docs/adr/`.
- Redactar los 5 ADRs fundacionales (retiro del prototipo .NET 6, vertical multi-tenancy, UUIDv7, Pure Handlers y Stack .NET 10 / Angular 22).
- Crear `docs/architecture/c4-model.md` con los diagramas C4 (Contexto L1, Contenedores L2 y Componentes L3) con clases `classDef`.
- Mover inventarios de seguridad legacy a `docs/reference/legacy-security/`.
- Prohibir archivos sueltos de lecciones aprendidas en `AGENTS.md`.

**Non-Goals:**
- Alterar la lógica de negocio ni código de la aplicación.
