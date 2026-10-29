# Spec Delta: docs-as-code-governance

## Purpose
Establece las directivas formales, la taxonomía Diátaxis, la gobernanza de registros de decisión de arquitectura (MADR v4), los estándares visuales C4 en Mermaid y las reglas de sincronización documental continua para el repositorio copystart.

## ADDED Requirements

### Requirement: Diátaxis Quadrant Structure
Toda la documentación técnica del repositorio copystart SHALL residir en el directorio `/docs/` y organizarse estrictamente en cuatro cuadrantes: `tutorials/`, `runbooks/`, `architecture/` y `reference/`.

#### Scenario: Document categorization adherence
- **WHEN** un desarrollador o agente crea un nuevo documento técnico
- **THEN** el documento debe ubicarse exclusivamente dentro de uno de los 4 subdirectorios Diátaxis según su función cognitiva.

### Requirement: Architecture Documentation (C4 Model)
El cuadrante `docs/architecture/` SHALL contener la especificación arquitectónica del sistema combinando diagramas del Modelo C4 (Contexto, Contenedores y Componentes) generados en sintaxis nativa de Mermaid Flowchart (`flowchart TB` / `flowchart TD`) utilizando clases declarativas `classDef` estables.

#### Scenario: Visual C4 diagram rendering
- **WHEN** se documenta la arquitectura del sistema CopyStart
- **THEN** debe utilizar sintaxis de Mermaid Flowchart con `classDef` representando los portales públicos de clientes, la consola técnica multi-inquilino y la API backend.

### Requirement: Architectural Decision Records Governance (MADR v4)
Todas las decisiones arquitecturales, de migración o diseño técnico en CopyStart SHALL formalizarse en archivos Markdown inmutables bajo `docs/adr/` siguiendo la plantilla Markdown Architectural Decision Records versión 4 (MADR v4).

#### Scenario: Structural change decision record
- **WHEN** se toma o modifica una decisión estructural de framework, base de datos o multi-tenancy
- **THEN** debe crearse un nuevo ADR numerado secuencialmente (`docs/adr/NNNN-titulo.md`) documentando contexto, opciones consideradas, decisión tomada y consecuencias.
