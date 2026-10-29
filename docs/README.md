# 📚 Documentación Técnica de CopyStart (Diátaxis)

Bienvenido al centro de documentación técnica y de arquitectura del proyecto **CopyStart SaaS**. Esta documentación sigue el estándar **Docs-as-Code**, organizado bajo los cuatro cuadrantes cognitivos de la taxonomía **Diátaxis**:

```
docs/
├── tutorials/       # 🎓 Aprendizaje: Guías de setup local (.NET 10, Angular 22, Docker)
├── runbooks/        # 🛠️ Procedimientos: Onboarding de talleres, despliegues y roadmap de entregables
├── architecture/    # 🏛️ Comprensión: Modelo C4 en Mermaid Flowchart y arquitectura SaaS
├── reference/       # 📖 Información: Inventarios de seguridad legacy, especificaciones de APIs y datos
└── adr/             # ⚖️ Decisiones: Architectural Decision Records en formato MADR v4
```

---

## 🧭 Cuadrantes Diátaxis

### 1. 🎓 Tutorials (Aprendizaje & Onboarding)
*Orientados al aprendizaje activo de desarrolladores.*
- Guías de desarrollo local y buenas prácticas de ingeniería en .NET 10 y Angular 22.

### 2. 🛠️ Runbooks (Procedimientos Operativos)
*Orientados a resolver tareas prácticas.*
- Procedimientos de onboarding de nuevos talleres y configuración de plantillas verticales.
- Roadmap de entregas y ciclo de vida de versiones.

### 3. 🏛️ Architecture (Comprensión del Sistema)
*Orientados a explicar el cómo y el por qué del software.*
- **[Modelo C4 & Arquitectura del Sistema (Mermaid Flowchart)](architecture/c4-model.md)**:
  - Nivel 1: Contexto del Sistema (Clientes, Técnicos, Administradores, Cloudflare, ZITADEL).
  - Nivel 2: Contenedores (Angular 22 SPA, CopyStart API .NET 10, PostgreSQL 17, SeaweedFS).
  - Nivel 3: Componentes de CopyStart API (Clean Architecture, WolverineFx, Multi-tenancy).

### 4. 📖 Reference (Información Pura & Seguridad)
*Orientados a la consulta técnica objetiva.*
- Especificaciones de datos y APIs.
- Informes de auditoría de seguridad y descarte de datos del prototipo legado.

---

## ⚖️ Architectural Decision Records (MADR v4)

Todas las decisiones estructurales del proyecto se encuentran versionadas e inmutables bajo [`docs/adr/`](adr/README.md):
- **[ADR-0001](adr/0001-retiro-prototipo-net6-y-descarte-migracion-datos.md)**: Retiro del Prototipo .NET 6 MVC y Descarte de Migración de Datos.
- **[ADR-0002](adr/0002-multi-tenancy-por-plantillas-verticales.md)**: Multi-Tenancy por Plantillas Verticales (Fotocopiadoras y Talleres).
- **[ADR-0003](adr/0003-identificadores-uuidv7-para-ordenes-y-activos.md)**: Identificadores UUIDv7 para Órdenes de Trabajo y Activos.
- **[ADR-0004](adr/0004-pure-handlers-wolverinefx-y-transaccionalidad-automatica.md)**: Pure Handlers con WolverineFx y Transaccionalidad Automática.
- **[ADR-0005](adr/0005-stack-tecnologico-canonico-dotnet10-angular22.md)**: Stack Tecnológico Canónico (.NET 10 LTS, Angular 22 Signals y PostgreSQL 17).
