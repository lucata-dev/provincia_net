# provincia_net — Instrucciones

Este repositorio contiene una API en .NET 8 con arquitectura por capas (API, Application, Domain, Infrastructure) y pruebas unitarias.

## Ejecución del proyecto

Requisitos:
- .NET 8 SDK
- Visual Studio 2022/2026 o dotnet CLI

Pasos básicos (línea de comandos):

1. Restaurar paquetes:

   dotnet restore

2. Construir la solución:

   dotnet build

3. Ejecutar la API (desde la raíz del repositorio):

   dotnet run --project API/API.csproj

   La API utiliza una base de datos en memoria (EF Core InMemory). Al arrancar se carga data dummy automáticamente (ver API/Program.cs).

4. Ejecutar las pruebas unitarias:

   dotnet test

## Detalle de las pruebas realizadas

- Proyecto de pruebas: Test
- Se añadieron tests unitarios (xUnit) que usan EF Core InMemory para verificar el comportamiento de los services:
  - AuthorServiceTests: CRUD completo (Create/Get/Update/Delete).
  - CategoryServiceTests: CRUD completo.
  - BookServiceTests: CRUD, LoanAsync (préstamo) y ReturnAsync (devolución) y comprobación de copias disponibles.
  - LoanServiceTests: Crear préstamo, marcar como devuelto y eliminar.

Las pruebas usan las implementaciones reales de repositorios (Infrastructure) con AppDbContext configurado en memoria, por simplicidad e integración ligera.

## Detalles de la arquitectura

- Capas:
  - API: controladores (API/Controllers/*) que exponen endpoints REST.
  - Application: servicios (Application/Services), DTOs (Application/DTOs) y validadores (FluentValidation en Application/Validators).
  - Domain: entidades y contratos (Domain/Entities, Domain/Interfaces).
  - Infrastructure: AppDbContext (EF Core), repositorios concretos (Infrastructure/Repositories).

- Patrones y tecnologías principales:
  - Inyección de dependencias configurada en API/Program.cs (registro de repositories y services).
  - EF Core con proveedor InMemory para ejecución local y pruebas.
  - Repositorios que encapsulan acceso a datos (Repository pattern).
  - Services en capa Application que implementan la lógica de negocio y mapean a DTOs.
  - FluentValidation para validación de DTOs registrada automáticamente (AddValidatorsFromAssemblyContaining).

## Notas y siguientes pasos recomendados

- Si desea persistencia real, reemplace UseInMemoryDatabase en API/Program.cs por un proveedor relacional (SQL Server, SQLite, etc.) y configure migrations.
- Los tests actuales usan implementaciones concretas; si se desea aislamiento, sustituir por mocks (p. ej. Moq).
- Para ejecutar la API desde Visual Studio, establecer API como proyecto de inicio y arrancar.
