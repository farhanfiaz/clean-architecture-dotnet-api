Clean Architecture .NET 10 Web API

A production-ready Clean Architecture Web API template built with .NET 10, designed for scalability, security, and maintainability.

It includes JWT Authentication, API Key security, Identity (Code-First), MediatR (CQRS), Swagger integration, CORS policy, and a custom mapping layer.

------------------------------------------------------------

## ✨ Key Features

- 🧱 Clean Architecture (Domain / Application / Infrastructure / API)
- ⚡ .NET 10 Web API
- 🔐 JWT Authentication & Authorization
- 🔑 API Key Authentication
- 👤 ASP.NET Core Identity (Code-First)
- 📡 MediatR (CQRS Pattern)
- 📘 Swagger UI with JWT & API Key support
- 🌐 CORS Policy configured
- 🔄 Entity Framework Core (Code-First Migrations)
- 🧠 Custom Mapper (Manual mapping layer)
- 📦 Domain Events support
- 🧩 Modular & scalable architecture

------------------------------------------------------------

## 📁 Project Structure

src/
  Domain
  Application
  Infrastructure
  API

------------------------------------------------------------

## ⚙️ Prerequisites

Make sure you have installed:

- .NET 10 SDK
- SQL Server (or any supported database)
- Visual Studio 2022 / VS Code / Rider

------------------------------------------------------------

## 🔐 AUTHENTICATION

### JWT Token
- Login via API
- Get token
- Use in Swagger:
  Authorization: Bearer YOUR_TOKEN

### API Key
x-api-key: YOUR_API_KEY

------------------------------------------------------------

SWAGGER

https://localhost:{port}/swagger

Supports:
- JWT Authentication
- API Key Authentication
- API testing UI

------------------------------------------------------------

DEFAULT FLOW

1. Run application
2. Register or seed user
3. Login to get JWT token
4. Authorize in Swagger
5. Test secure endpoints

------------------------------------------------------------

SETUP CHECKLIST

- Update connection string
- Configure JWT settings
- Set API Key
- Run migrations
- Configure CORS

------------------------------------------------------------

NOTES

- Built for enterprise-level applications
- Clean separation of concerns
- Easily extendable architecture
- Supports microservice transition

------------------------------------------------------------

## 🚀 Getting Started

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/your-repo-name.git
cd your-repo-name
------------------------------------------------------------

2. Configure Database (appsettings.json)

"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;TrustServerCertificate=True;"
}

------------------------------------------------------------

3. Configure JWT

"JwtSettings": {
  "Key": "YOUR_SECRET_KEY",
  "Issuer": "YourIssuer",
  "Audience": "YourAudience",
  "DurationInMinutes": 60
}

------------------------------------------------------------

4. Configure API Key

"ApiKey": "YOUR_API_KEY"

------------------------------------------------------------

5. Run Migrations

dotnet ef database update

If migrations not created:

dotnet ef migrations add InitialCreate
dotnet ef database update

------------------------------------------------------------
