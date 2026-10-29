# 🏛️ Modelo C4 & Arquitectura del Sistema (CopyStart)

Este documento define la arquitectura formal del sistema **CopyStart SaaS**, utilizando el **Modelo C4** diagramado mediante Mermaid Flowchart estándar con clases declarativas `classDef` universalmente compatibles.

---

## 1. Nivel 1: Diagrama de Contexto del Sistema (System Context)

```mermaid
flowchart TB
    %% Clases de estilo C4
    classDef boundary fill:none,stroke:#555,stroke-width:2px,stroke-dasharray: 5 5;
    classDef person fill:#084298,stroke:#051c42,color:#fff;
    classDef internalSystem fill:#198754,stroke:#0f5132,color:#fff;
    classDef externalSystem fill:#495057,stroke:#212529,color:#fff;

    Cliente["Cliente Final<br/>[Persona]<br/>Solicita servicios tecnicos, consulta estado y aprueba cotizaciones"]:::person
    Tecnico["Tecnico de Campo / Taller<br/>[Persona]<br/>Diagnostica activos, registra partes y ejecuta ordenes de trabajo"]:::person
    Administrador["Administrador del Taller / Empresa<br/>[Persona]<br/>Gestiona tecnicos, contratos de renting, inventario y facturacion"]:::person

    subgraph PerimetroEdge["Perimetro Seguro Edge"]
        Cloudflare["Cloudflare Zero Trust / Tunnel<br/>[Servicio Externo]<br/>WAF, SSL *.copystart.co y Dominios Personalizados"]:::externalSystem
    end

    subgraph SistemaCopyStart["Límite del Sistema: CopyStart SaaS Multi-Tenant"]
        CopyStartSystem["Plataforma CopyStart<br/>[Sistema Software]<br/>Portal publico de clientes, consola tecnica y motor de ordenes de trabajo"]:::internalSystem
    end

    subgraph ServiciosExternos["Servicios Externos Integrados"]
        ZitadelIdP["ZITADEL IdP<br/>[Identidad OIDC]<br/>Autenticacion OIDC, roles y politicas de login por empresa"]:::externalSystem
        StorageS3["SeaweedFS S3 Storage<br/>[Almacenamiento S3]<br/>Evidencias fotograficas de danos, manuales tecnicos y adjuntos"]:::externalSystem
    end

    Cliente -->|"HTTPS / Web Portal Cliente"| Cloudflare
    Tecnico -->|"HTTPS / Consola Movil / Web"| Cloudflare
    Administrador -->|"HTTPS / Panel de Administracion"| Cloudflare

    Cloudflare -->|"Tunel Cifrado"| CopyStartSystem

    CopyStartSystem -->|"Autenticacion OIDC PKCE"| ZitadelIdP
    CopyStartSystem -->|"Evidencias Fotograficas y Documentos"| StorageS3

    class PerimetroEdge,SistemaCopyStart,ServiciosExternos boundary;
```

---

## 2. Nivel 2: Diagrama de Contenedores (Container Diagram)

```mermaid
flowchart TB
    %% Clases de estilo C4
    classDef boundary fill:none,stroke:#555,stroke-width:2px,stroke-dasharray: 5 5;
    classDef webApp fill:#d32f2f,stroke:#b71c1c,color:#fff;
    classDef backendApp fill:#512bd4,stroke:#311b92,color:#fff;
    classDef database fill:#336791,stroke:#1a365d,color:#fff;
    classDef storage fill:#37474f,stroke:#212121,color:#fff;

    subgraph FrontendClients["Aplicaciones Cliente"]
        AngularSPA["Frontend Angular 22 Client SPA<br/>[Angular 22, Signals]<br/>Portal de clientes, consola tecnica responsiva y adaptacion por plantilla vertical"]:::webApp
    end

    subgraph BackendServices["Backend y Logica de Negocio"]
        DotNetAPI["CopyStart Web API .NET 10 LTS<br/>[C# 14, Clean Architecture, WolverineFx]<br/>Orquestacion de ordenes de trabajo, inventarios, telemetria y multi-tenancy"]:::backendApp
    end

    subgraph StorageData["Persistencia y Almacenamiento"]
        PostgresDB[("Base de Datos Principal<br/>[PostgreSQL 17 StatefulSet]<br/>Almacen relacional multi-tenant, indices UUIDv7 para ordenes y activos")]:::database
        S3Storage["SeaweedFS S3 Storage<br/>[SeaweedFS S3]<br/>Fotos de peritajes, firmas digitales y reportes en PDF"]:::storage
    end

    AngularSPA -->|"HTTPS / REST JSON Host: empresa.copystart.co"| DotNetAPI
    DotNetAPI -->|"TCP / Port 5432 TenantSaveChangesInterceptor"| PostgresDB
    DotNetAPI -->|"HTTP / S3 API PreSigned URLs"| S3Storage

    class FrontendClients,BackendServices,StorageData boundary;
```

---

## 3. Nivel 3: Diagrama de Componentes (Component Diagram - CopyStart API)

```mermaid
flowchart TD
    %% Clases de componentes
    classDef presentation fill:#e3f2fd,stroke:#1565c0,color:#0d47a1;
    classDef application fill:#ede7f6,stroke:#512da8,color:#311b92;
    classDef domain fill:#e8f5e9,stroke:#2e7d32,color:#1b5e20;
    classDef infrastructure fill:#fff3e0,stroke:#e65100,color:#bf360c;

    subgraph PresentationLayer["Capa de Presentacion Presentation"]
        Controllers["Controllers / Endpoints<br/>OrdenesTrabajoController, ActivosController, ClientesController"]:::presentation
        TenantMiddleware["TenantMiddleware<br/>Resuelve Tenant via Host o Header X-Tenant-Id"]:::presentation
    end

    subgraph ApplicationLayer["Capa de Aplicacion Application"]
        WolverineBus["WolverineFx Bus y Pipeline"]:::application
        CommandHandlers["Command Handlers<br/>CrearOrdenTrabajoHandler, ConsumirParteHandler, CerrarOrdenHandler"]:::application
        QueryHandlers["Query Handlers<br/>ObtenerOrdenPorNumeroQuery, ListarActivosPorClienteQuery"]:::application
        Mapperly["Riok.Mapperly Mappers"]:::application
    end

    subgraph DomainLayer["Capa de Dominio Domain"]
        Entities["Entidades de Dominio UUIDv7<br/>Tenant, OrdenTrabajo, Activo, ParteInventario, Tecnico"]:::domain
        DomainServices["WorkOrderDomainServices"]:::domain
    end

    subgraph InfrastructureLayer["Capa de Infraestructura Infrastructure"]
        AppDbContext["CopyStartDbContext EF Core 10"]:::infrastructure
        TenantInterceptor["TenantSaveChangesInterceptor"]:::infrastructure
    end

    TenantMiddleware --> Controllers
    Controllers --> WolverineBus
    WolverineBus --> CommandHandlers
    WolverineBus --> QueryHandlers
    CommandHandlers --> Entities
    CommandHandlers --> Mapperly
    CommandHandlers --> AppDbContext
    QueryHandlers --> AppDbContext
    AppDbContext --> TenantInterceptor
```
