# Proposal: Docs-as-Code Governance Standardization in CopyStart

## Why
El repositorio `copystart` implementa una solución SaaS multi-tenant para empresas de servicio técnico, talleres mecánicos y mantenimiento de activos (fotocopiadoras, vehículos, maquinaria). Actualmente, su documentación técnica bajo `docs/` se encuentra dispersa en carpetas ad-hoc (`architecture/`, `delivery/`, `development/`, `security/`, `agent-workflows/`) y coexiste con el prototipo legado en .NET 6 MVC.

Estandarizar el proyecto a la tríada **Docs-as-Code (Diátaxis + MADR v4 + Modelo C4)** permite formalizar el desacoplamiento del prototipo obsoleto, establecer la arquitectura limpia en .NET 10 LTS y Angular 22, consolidar el aislamiento multi-tenant y optimizar la gobernanza para desarrolladores y agentes de IA.

## What Changes
- **Taxonomía Diátaxis (`/docs/`)**:
  - `docs/tutorials/`: Guías de inicio y desarrollo local para el nuevo SaaS (.NET 10, Angular 22, Docker, PostgreSQL).
  - `docs/runbooks/`: Procedimientos operativos (onboarding de talleres/empresas inquilinas, migraciones de base de datos, pruebas E2E).
  - `docs/architecture/`: Especificación integral de arquitectura unificando el Modelo C4 en Mermaid Flowcharts (`classDef` estándar) con el modelo de dominio de órdenes de trabajo, activos y servicios técnicos.
  - `docs/reference/`: Especificaciones técnicas de APIs, contratos DTO, esquema de base de datos y auditoría de seguridad del prototipo legacy.
- **Registro de Decisiones de Arquitectura (MADR v4 en `docs/adr/`)**:
  - `ADR-0001`: Retiro del prototipo .NET 6 MVC y descarte de migración de datos legacy.
  - `ADR-0002`: Multi-tenancy por plantillas verticales (fotocopiadoras vs. talleres automotrices).
  - `ADR-0003`: Identificadores ordenados cronológicamente con UUIDv7 nativo en .NET 10.
  - `ADR-0004`: Patrón Pure Handlers con WolverineFx y gestión transaccional automática.
  - `ADR-0005`: Stack tecnológico canónico (.NET 10 LTS Web API, Angular 22 Signals, PostgreSQL 17).
- **Consolidación de `docs/` existente**: Reclasificar los directorios previos bajo Diátaxis preservando los informes de seguridad e inventarios legacy en `docs/reference/`.
- **Gobernanza en `AGENTS.md`**: Actualizar las directivas para prohibir archivos sueltos de lecciones aprendidas y fijar el estándar Diátaxis + MADR v4.

## Capabilities

### New Capabilities
- `docs-as-code-governance`: Reglas formales de gobernanza documental, estructura Diátaxis, ciclo de vida de ADRs en MADR v4, estándares C4 en Mermaid y sincronización continua.

### Modified Capabilities
*Ninguna.*

## Impact
- **Estructura de archivos**: Reorganización de `/docs/` en cuadrantes Diátaxis y creación de `docs/adr/`.
- **Código de producción**: Cero impacto en código C# o TypeScript.
