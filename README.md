# User and Address Management API (.NET 8)

## Overview

This project is a modern Web API built with **ASP.NET Core 8**, designed to manage user and address data. It utilizes **Dapper** for high-performance data access, **AutoMapper** for object mapping, and follows the **Repository** and **Unit of Work** patterns for clean architecture.
This project also integrates with the public API [JSONPlaceholder](https://jsonplaceholder.typicode.com/) to fetch external user data.

---

## Frameworks and Libraries Used

### ✅ .NET 8 / ASP.NET Core
- Latest version of .NET for building fast, minimal, and scalable web APIs.
- Provides built-in dependency injection, minimal hosting model, and modern API development features.

### ✅ Dapper
- Lightweight and high-performance micro-ORM.
- Used for executing raw SQL queries efficiently without the overhead of Entity Framework.
- Ideal for CRUD operations and fine-tuned query control.

### ✅ AutoMapper
- Automatically maps between domain models and DTOs.
- Reduces repetitive mapping code and increases maintainability.

### ✅ HttpClient Factory
- Used to call external APIs with managed `HttpClient` instances.

### ✅ Dependency Injection
- Built into ASP.NET Core to manage services and repository lifetimes.
- Encourages loose coupling and makes unit testing easier.

---

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or full edition)
- [Git](https://git-scm.com/downloads)

---
## Setup Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/nadee-harshani/liquidlabs-user-service.git
cd liquidlabs-user-service
```

### 2 . Database Setup
- Make sure you have SQL Server installed and running.
- Run the SQL script (if provided) to create databse and necessary tables.
```bash
  sqlcmd -S (localdb)\MSSQLLocalDB -i ./Database/db_schema.sql
```
- Replace (localdb)\MSSQLLocalDB with your SQL Server instance if needed.

### 3. Configure the Application
- Update the connection string in appsettings.json if your SQL Server instance or credentials:
```json
{
  "ConnectionStrings": {
    "DatabaseConnection": "Server=DESKTOP-J2M779Q; Initial Catalog=LiquidLabs;TrustServerCertificate=True;"
  }
}
```
### 4. Build and Run the API
- Build and run the API using the .NET CLI:
```bash
dotnet build
dotnet run --project LiquidLabs.UserService.API
```
---
##  External API Integration
This application integrates with the public API https://jsonplaceholder.typicode.com/ to retrieve mock user data.

Used in ExternalUserService via HttpClient.

---

## Project Structure
| Layer      | Description                                                                                   |
|------------|-----------------------------------------------------------------------------------------------|
| API        | Hosts the Web API endpoints. Handles HTTP requests and responses using controllers.           |
| Services   | Contains business logic and data access through repositories.                    |
| DataAccess | Implements repository and unit of work patterns using Dapper. Manages SQL and transactions.   |
| Domain     | Contains core domain models, and interfaces (contracts) for services and repositories.        |
| Database   | Includes SQL scripts (`db_schema.sql`) to create the database and tables. 

---

##  API Endpoints
| Method | Endpoint                    | Description                            |
|--------|-----------------------------|----------------------------------------|
| GET    | `/api/Users`               | Get all users from the local database  |
| GET    | `/api/Users/{id}`          | Get a specific user by ID              |

---
## API Key Protection

This APIs are secured using an **API Key mechanism**. All clients must send a valid API key in the request headers to access protected endpoints.

### Required Header

Clients must include the following HTTP header with each request:
```http
X-Api-Key: userservice@apikey
```
---

## Testing the API
- You can test the API using the following tools:
  #### 1. Swagger UI
  After running the API, open your browser and navigate to swagger UI. ex: https://localhost:7192/swagger/
  #### 2. Postman
  You can also import requests into Postman and test.
---
## ✅ Features
- Fetch users from both internal SQL database and external API.
- Add new users and addresses.
- Layered architecture (API, Service, Repository, Domain).
- Database initialization via SQL script.
- Uses Dapper for clean and efficient SQL execution.










