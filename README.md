
# Digital Bank Assessment

Aplicación para el manejo y gestión de usuarios. La solución incluye un backend en .NET, un frontend en Angular y una base de datos SQL Server. Se proporciona soporte para ejecución con `docker-compose` o de forma manual.

## Contenido del proyecto

- `db/init-db/`: Scripts y configuración para inicializar la base de datos (Ejecutados automáticamente si usas docker, o manualmente si no)
- `postman/`: Colección postman
- `DigitalBankAssessment-backend/`: Backend en ASP.NET Core Web API
- `DigitalBankAssessment-frontend/`: Frontend en Angular 19
- `README.md`: Instrucciones del proyecto
- `docker-compose.yml`: Archivo de orquestación de contenedores
---

## Tecnologías utilizadas

### Backend

- .NET Core 9
- ASP.NET Core Web API
- Entity Framework Core (Database-First)
- MediatR para CQRS
- SQL Server
- Docker

### Frontend

- Angular 19
- Angular Material
- Docker

### Base de Datos

- SQL Server
- Scripts incluidos:
  - `01-createdb.sql`: Creación de base de datos y tablas
  - `02-createsp.sql`: Creación de Stored Procedures
- Docker

---
## Postman

En la raíz del repositorio se encuentra una carpeta llamada /postman. Dentro encontrarás la siguiente colección
- DigitalBankAssessment.API.postman_collection

Puedes importarla en Postman u otra plataforma API

## Primer Paso
### Clonar repositorio
```bash

git  clone  repooo.com

cd  DigitalBankAssessment

```
---
Puedes ejecutar el proyecto de dos maneras:

## 1. Ejecutar el proyecto  con Docker Compose

### Requisitos
- Docker

### Pasos

1   .  Ejecuta Docker Compose:

  

```bash

docker-compose  up  --build

```

  

Esto construirá y levantará los siguientes servicios:

  

-  **db**: Contenedor con SQL Server y los scripts necesarios ya ejecutados.

-  **api_container**: Web API ASP.NET Core.
-  **webapp_container**: Aplicación web Angular.

  

2   .  **Acceso a los servicios:**

| API | http://localhost:5000 |
| Web App | http://localhost:4200/ |

| SQL Server | localhost:1433 (usuario: `sa`, password: `digitalBank456!`) |
  

---

  

## 2. Ejecutar el proyecto manualmente (sin Docker)

  

### 1. Configurar la base de datos

  

- Asegúrate de tener SQL Server instalado y en ejecución.

- Ejecuta los siguientes scripts en orden:

    1.  `01-createdb.sql`

    2.  `02-createsp.sql`

  

### 2. Ejecutar el backend

  

```bash

cd  DigitalBankAssessment-backend

dotnet  restore

dotnet  build

cd DigitalBankAssessment.API
dotnet  run

```

  

Verifica que esté escuchando en `http://localhost:5000`.

  

### 3. Ejecutar el frontend
Abra otra terminal, dentro de DigitalBankAssessment ejecute:
  

```bash

cd  DigitalBankAssessment-frontend

npm  install

ng  serve

```

  

La aplicación estará disponible en `http://localhost:4200`.


## Notas

  

- El backend usa DB First con Entity Framework Core.

- Los scripts deben ser ejecutados antes de usar la aplicación si no se usa Docker.

- La entidad `Usuario` incluye el campo `Activo` que indica si el usuario está habilitado.
- El endpoint `DELETE /api/users/{id}` no elimina físicamente al usuario, sino que realiza una **borrado lógico**.
- Para volver a activarlo, puede implementarse un endpoint de reactivación en el futuro.

