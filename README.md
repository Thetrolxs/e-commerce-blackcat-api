# 🛒 e-commerce-blackcat-api

**API REST desarrollada como parte del Taller de Introducción al Desarrollo Web Móvil.**  
Esta aplicación representa el backend de una plataforma de comercio electrónico, permitiendo gestionar productos, usuarios, carritos de compra, órdenes y direcciones de envío.

---

## 📚 Descripción del proyecto

Este proyecto consiste en el desarrollo de una API RESTful utilizando **.NET 9** y **Entity Framework Core**, orientada a la gestión de un sistema de comercio electrónico.  
La arquitectura sigue buenas prácticas de diseño, incluyendo los patrones **Repository** y **Unit of Work**, lo que permite una separación clara de responsabilidades y facilita el mantenimiento y escalabilidad del sistema.

La API implementa lógica CRUD para:

- 🧴 Productos
- 👤 Usuarios (con dirección de envío)
- 🛒 Carrito de compras
- 📦 Órdenes y detalle de órdenes

El foco del desarrollo actual está en la estructuración del backend para futuras integraciones frontend.

---

## 🧑‍💻 Autores

- **Ignacio Alfonso Morales Harnisch**

    Correo: ignacio.morales01@alumnos.ucn.cl
  
    RUT: 20.823.511-7
  
- **Alonso Antonio Rojas Valdovino**

    Correo: alonso.rojas@alumnos.ucn.cl

    RUT: 21.059.748-4
---

## 🧱 Tecnologías utilizadas

- [.NET 9](https://learn.microsoft.com/en-us/dotnet/core/whats-new/dotnet-9)
- Entity Framework Core
- SQLite
- ASP.NET Core Identity
- C#
- Patrones: Repository, Unit of Work
- Bogus

---

## 🗂️ Estructura del proyecto

```
Src/
│
├── Controllers/        → Controladores donde se encuentran los endpoints
├── Data/               → DataContext, DbInitializer, Migraciones EF
├── Dtos/               → Clases para transferencia de datos (UserDto, etc.)
├── Helpers/            → Archivo con ayudas de validaciones y mappingprofile
├── Mappers/            → Conversión entre modelos y DTOs
├── Models/             → Entidades del dominio: Product, User, CartItem, etc.
├── Repositories/       → Implementaciones de lógica de acceso a datos
├── Services/           → Servicios que interactuan con los controladores
├── Program.cs          → Configuración general del servidor y servicios
```

---

## ⚙️ Cómo ejecutar el proyecto

### 1. Clonar el repositorio

```bash
git clone https://github.com/Thetrolxs/e-commerce-blackcat-api.git
cd e-commerce-blackcat-api
```

### 2. Restaurar dependencias

```bash
dotnet restore
```

### 3. Aplicar migraciones a la base de datos

```bash
dotnet ef database update
```

> ⚠️ Asegúrate de tener instalada la herramienta `dotnet-ef` si no la tienes:
```bash
dotnet tool install --global dotnet-ef
```
### 4. Agregar el appsettings.json
agregar el siguiente codigo en la carpeta principal del proyecto con nombre appsettings.json
```bash
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",

  "ConnectionStrings": {
    "DefaultConnection": "Data Source=blackcat.db"
  },

  "JwtSettings": {
    "Key": "ThisIsASecretKeyWith256BitMinimumLength1234", 
    "Issuer": "ECommerceApi",
    "Audience": "ECommerceClient",
    "ExpiresInMinutes": 60
  },

  "CloudinarySettings": {
    "CloudName": "tu-cloud-name",
    "ApiKey": "tu-api-key",
    "ApiSecret": "tu-api-secret"
  },

  "CorsSettings": {
    "AllowedOrigins": [
      "https://localhost:7195",
      "http://localhost:3000"
    ],
    "AllowedMethods": [ "GET", "POST", "PUT", "DELETE" ],
    "AllowedHeaders": [ "Content-Type", "Authorization" ]
  },

  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "Enrich": [ "FromLogContext", "WithThreadId", "WithMachineName" ],
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "Logs/log-.txt",
          "rollingInterval": "Day",
          "outputTemplate": "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
        }
      }
    ]
  }
}

```
---
### 5. Ejecutar la aplicación

```bash
dotnet run
```

La API se iniciará en `https://localhost:7195/` según lo definido en `appsettings.json`.

---

## 🎓 Contexto académico

Este proyecto fue desarrollado como entrega del Taller de:
> **Introducción al Desarrollo Web Móvil**  
> **Universidad Católica del Norte**

---

## 📝 Licencia

Este proyecto ha sido desarrollado exclusivamente con fines académicos.  
No está habilitado para distribución o uso comercial.
