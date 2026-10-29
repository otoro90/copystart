# Identificadores UUIDv7 para Órdenes de Trabajo y Activos

* Status: accepted
* Deciders: Platform Owner & Architecture Team
* Date: 2026-10-29

Technical Story: [High-Performance Primary Keys & Sequential Indexing]

## Context and Problem Statement

El sistema gestiona millones de eventos de telemetría, lecturas de contadores, estados de órdenes de trabajo e historiales de partes. Los enteros secuenciales (`BIGSERIAL`) exponen volúmenes de negocio a la competencia en URLs, mientras que los UUIDv4 aleatorios degradan los índices B-Tree en PostgreSQL 17.

## Decision Drivers

* Orden cronológico nativo para paginación y auditoría de servicios técnicos.
* Inserciones eficientes al final del índice B-Tree (cero page-splitting).
* Pre-generación de IDs en memoria con `Guid.CreateVersion7()`.

## Considered Options

* **Opción 1: UUIDv7 nativo en .NET 10 (Elegida)**: 48 bits de timestamp Unix combinados con aleatoriedad criptográfica bajo el estándar RFC 9562.
* **Opción 2: UUIDv4 tradicional**: Aleatoriedad pura sin noción temporal.
* **Opción 3: Secuencias enteras**: Claves numéricas continuas.

## Decision Outcome

Chosen option: **Opción 1: UUIDv7 nativo en .NET 10**, because combina privacidad comercial, orden natural de eventos y máximo rendimiento de escritura en PostgreSQL 17.
