# Retiro del Prototipo .NET 6 MVC y Descarte de Migración de Datos

* Status: accepted
* Deciders: Platform Owner & Architecture Team
* Date: 2026-10-29

Technical Story: [Legacy Prototype Retirement & Clean Greenfield SaaS Baseline]

## Context and Problem Statement

El repositorio contiene un prototipo original desarrollado en .NET 6 MVC (`net6.0`), cuyo soporte oficial finalizó el 12 de noviembre de 2024. El prototipo utilizaba ASP.NET Identity local sin multi-inquilino, un único `ApplicationDbContext` mono-empresa, persistencia local en disco para archivos y vistas Razor acopladas a AdminLTE.

Adicionalmente, se identificó que credenciales de PostgreSQL del prototipo quedaron expuestas en el historial de Git antiguo (las cuales han sido rotadas y confirmadas como fuera de producción).

¿Debemos intentar migrar el código y los datos del prototipo .NET 6 al nuevo SaaS, o iniciar una base de código limpia?

## Decision Drivers

* Eliminación de deuda técnica y fin de soporte de .NET 6.
* Cero arrastre de credenciales o esquemas de base de datos comprometidos históricamente.
* Adopción de arquitectura multi-tenant moderna en la nube.

## Considered Options

* **Opción 1: Retiro total del prototipo y arranque greenfield sin migración de datos (Elegida)**: El prototipo se conserva únicamente como evidencia de descubrimiento de producto (reglas de negocio y flujos). El nuevo SaaS inicia con un esquema limpio, sin importar registros legados.
* **Opción 2: Actualización in-place de .NET 6 a .NET 10**: Refactorizar el código existente.

## Decision Outcome

Chosen option: **Opción 1: Retiro total del prototipo y arranque greenfield**, because el prototipo nunca estuvo en producción real con clientes multi-tenant, carece de abstracciones de inquilino y su refactorización sería más costosa que implementar la arquitectura limpia directamente.
