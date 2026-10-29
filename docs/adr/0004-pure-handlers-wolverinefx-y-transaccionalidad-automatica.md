# Pure Handlers con WolverineFx y Transaccionalidad Automática

* Status: accepted
* Deciders: Platform Owner & Architecture Team
* Date: 2026-10-29

Technical Story: [Transaction Management & Pure Business Handlers]

## Context and Problem Statement

El ciclo de una orden de trabajo involucra cambios de estado, consumo de partes de inventario, registro de tiempos de técnicos y generación de cobros. Mezclar llamadas a `SaveChangesAsync()` dentro de la lógica del caso de uso genera commits parciales si una validación posterior falla.

## Decision Drivers

* Atomicidad transaccional total (todo o nada).
* Código desacoplado de la infraestructura de persistencia.
* Interceptores multi-tenant consistentes para inyectar `TenantId`.

## Considered Options

* **Opción 1: WolverineFx con RequestTransactionMiddleware (Elegida)**: Los command handlers son funciones puras sin inyección de `IUnitOfWork`. La transacción se confirma automáticamente al terminar exitosamente el handler o se revierte ante excepciones.
* **Opción 2: Commit manual en cada Handler**: Inyección de `DbContext` y llamadas manuales.

## Decision Outcome

Chosen option: **Opción 1: WolverineFx con RequestTransactionMiddleware**, because elimina errores humanos de transacciones incompletas y mantiene los casos de uso limpios y testeables.
