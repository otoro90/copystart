# Architectural Decision Records (ADRs) - CopyStart

Este directorio mantiene el registro inmutable de decisiones técnicas y arquitecturales de **CopyStart**, siguiendo la especificación formal **Markdown Architectural Decision Records versión 4 (MADR v4)**.

---

## 📚 Índice de Decisiones de Arquitectura

| ADR | Título | Estado | Fecha | Contexto / Impacto |
| :--- | :--- | :---: | :---: | :--- |
| **[ADR-0001](0001-retiro-prototipo-net6-y-descarte-migracion-datos.md)** | Retiro del Prototipo .NET 6 MVC y Descarte de Migración de Datos | `accepted` | 2026-10-29 | Fin de ciclo de vida de .NET 6 y arranque limpio del nuevo SaaS |
| **[ADR-0002](0002-multi-tenancy-por-plantillas-verticales.md)** | Multi-Tenancy por Plantillas Verticales (Fotocopiadoras y Talleres) | `accepted` | 2026-10-29 | Aislamiento por tenant y workflows configurables según industria |
| **[ADR-0003](0003-identificadores-uuidv7-para-ordenes-y-activos.md)** | Identificadores UUIDv7 para Órdenes de Trabajo y Activos | `accepted` | 2026-10-29 | Claves primarias cronológicas generadas en memoria con `Guid.CreateVersion7()` |
| **[ADR-0004](0004-pure-handlers-wolverinefx-y-transaccionalidad-automatica.md)** | Pure Handlers con WolverineFx y Transaccionalidad Automática | `accepted` | 2026-10-29 | Prohibición de SaveChanges manual en handlers y commits atómicos |
| **[ADR-0005](0005-stack-tecnologico-canonico-dotnet10-angular22.md)** | Stack Tecnológico Canónico (.NET 10 LTS, Angular 22 Signals y PostgreSQL 17) | `accepted` | 2026-10-29 | Plataforma unificada de alto rendimiento y bajo consumo de memoria |

---

## 🛠️ Convención y Plantilla Base

Cada nuevo registro sigue el formato MADR v4 y se titula secuencialmente con cuatro dígitos (`NNNN-titulo.md`). Estados válidos: `proposed`, `accepted`, `rejected`, `deprecated`, `superseded`.
