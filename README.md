# StudentNotesApi# StudentNotesApi

## Overview
StudentNotesApi is a RESTful ASP.NET Core Web API for managing students, their subjects, and notes.
It supports full CRUD operations and uses DTOs so API responses differ from database entities.

## Tech Used
- ASP.NET Core Web API
- EF Core
- SQLite
- Swagger (OpenAPI)
- JWT Authentication (Register/Login)

## Database
This project uses SQLite persistent storage.

### Run migrations
Open Package Manager Console and run:
- Add-Migration InitialCreate
- Update-Database

Or just run Update-Database if migrations already exist.

## Run the API
1. Open the solution in Visual Studio
2. Press ▶ Run
3. Open Swagger:
https://localhost:7267/swagger/index.html
## Authentication (JWT)
### Register
POST /api/Auth/register
```json
{ "username": "test", "password": "Pass123!" }
Students 
GET /api/students 
POST /api/students  
{
  "name": "Amena",
  "email": "amena@gmail.com"
}
 GET /api/Students/{id}
1
PUT /api/Students/{id}  1 
{
  "name": "Amena Updated",
  "email": "amena.updated@gmail.com"
}

Auth
/api/Auth/register
{ "username": "test", "password": "Pass123!" }
