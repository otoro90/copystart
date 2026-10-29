# Multi-Tenancy por Plantillas Verticales (Fotocopiadoras y Talleres)

* Status: accepted
* Deciders: Platform Owner & Architecture Team
* Date: 2026-10-29

Technical Story: [Vertical Industry Templates & Multi-Tenant Isolation]

## Context and Problem Statement

CopyStart sirve a dos tipos principales de empresas piloto:
1. Empresas de mantenimiento y alquiler de fotocopiadoras/impresoras (lecturas de contadores, tóner, contratos de renting).
2. Talleres mecánicos y automotrices (placas de vehículos, kilometraje, repuestos y peritajes).

Aunque ambas industrias operan mediante Solicitudes, Órdenes de Trabajo, Activos y Partes, sus terminologías y etapas difieren sensiblemente.

¿Cómo diseñamos un sistema multi-tenant que sirva a múltiples verticales sin fragmentar la base de datos ni crear aplicaciones separadas?

## Decision Drivers

* Aislamiento estricto de datos entre empresas clientes.
* Flexibilidad en terminología y estados de flujo de trabajo.
* Economía de escala: una sola API y una sola base de datos PostgreSQL compartida.

## Considered Options

* **Opción 1: Multi-Tenancy con Plantillas Verticales Versionadas (Elegida)**: Cada inquilino (`Tenant`) tiene asignada una plantilla vertical (`VerticalTemplate`: Copiadoras, Automotriz, Maquinaria). La UI y las reglas de negocio adaptan labels, campos de activo y estados a partir de la configuración del inquilino resuelto por subdominio.
* **Opción 2: Múltiples aplicaciones monorrepo independientes**: Desplegar un servicio por industria.

## Decision Outcome

Chosen option: **Opción 1: Multi-Tenancy con Plantillas Verticales Versionadas**, because maximiza la reutilización del motor transaccional de órdenes de trabajo, reduciendo drásticamente los costos operativos.
