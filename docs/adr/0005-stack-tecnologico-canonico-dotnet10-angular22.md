# Stack Tecnológico Canónico (.NET 10 LTS, Angular 22 Signals y PostgreSQL 17)

* Status: accepted
* Deciders: Platform Owner & Architecture Team
* Date: 2026-10-29

Technical Story: [Technology Stack Standardization]

## Context and Problem Statement

Para el nuevo SaaS de CopyStart se requiere consolidar un stack tecnológico moderno, robusto y alineado con los demás productos del ecosistema Forjanova (GovCore y Ecclesiae).

## Decision Drivers

* Ecosistema unificado y reutilización de conocimientos de Clean Architecture.
* Rendimiento en arquitecturas multi-tenant.
* Soporte a largo plazo (LTS).

## Considered Options

* **Opción 1: .NET 10 LTS + Angular 22 Signals + PostgreSQL 17 (Elegida)**: Arquitectura limpia en C# 14, EF Core 10, WolverineFx y Angular Standalone con Signals.
* **Opción 2: Stack heterogéneo (Node.js / Go)**: Desarrollar con frameworks no utilizados en el ecosistema.

## Decision Outcome

Chosen option: **Opción 1: .NET 10 LTS + Angular 22 + PostgreSQL 17**, because maximiza la velocidad de desarrollo y garantiza sinergia total con la infraestructura de despliegue en K3s ARM64.
