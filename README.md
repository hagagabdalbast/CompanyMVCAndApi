# Company Management — ASP.NET Core (MVC + Web API)

> A company & employee management system built with **ASP.NET Core**, demonstrating a clean **layered architecture** with a shared business and data-access core powering both an **MVC web app** and a **RESTful Web API**.

![C#](https://img.shields.io/badge/C%23-239120?style=flat&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=flat&logo=dotnet&logoColor=white)
![EF Core](https://img.shields.io/badge/Entity%20Framework%20Core-512BD4?style=flat&logo=dotnet&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=flat&logo=microsoftsqlserver&logoColor=white)

## 🏛️ Architecture

The solution is organized into clearly separated layers for maintainability and testability:

| Project | Responsibility |
| --- | --- |
| **Demo** | MVC presentation layer (Razor views, controllers) |
| **Demo.API** | RESTful Web API exposing the same domain |
| **Demo.BL** | Business Logic layer — services, repositories, interfaces, DTOs |
| **Demo.DAL** | Data Access layer — Entity Framework Core, entities, migrations |

This separation follows the **Repository** and **Service Layer** patterns with **Dependency Injection**, keeping the presentation layers thin and the domain logic reusable across both MVC and API.

## 🛠️ Tech Stack

- **Language:** C#
- **Framework:** ASP.NET Core (MVC + Web API)
- **ORM:** Entity Framework Core
- **Database:** SQL Server

## 🚀 Getting Started

```bash
# 1. Restore dependencies
dotnet restore

# 2. Update the connection string in appsettings.json

# 3. Apply database migrations (from the DAL/startup project)
dotnet ef database update

# 4. Run the MVC app or the API
dotnet run --project Demo
dotnet run --project Demo.API
```

## 📁 Solution Structure

```text
Demo/        # MVC web application
Demo.API/    # Web API
Demo.BL/     # Business logic (services, repositories, DTOs)
Demo.DAL/    # Data access (EF Core, entities, migrations)
Demo.sln     # Solution file
```

---

<p align="center">Built by <a href="https://github.com/hagagabdalbast">Hagag Abdelbaset</a></p>
