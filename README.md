# Hospital Management System API

A comprehensive multi-tenant hospital management system built with .NET 8 and Clean Architecture.

## Architecture

The solution follows Clean Architecture principles with 5 layers:

- **HospitalAPI.Domain**: Core entities and repository interfaces
- **HospitalAPI.Application**: Business logic, DTOs, and service interfaces
- **HospitalAPI.Infrastructure**: Data access, EF Core, and repository implementations
- **HospitalAPI.API**: Web API controllers and middleware
- **HospitalAPI.Common**: Shared utilities, constants, and helpers

## Features

### Multi-Tenant Support
- Tenant isolation at application level
- All entities scoped to tenant_id

### Modules
- **IAM**: User management with custom RBAC
- **Organization**: Tenants, Branches, Facilities, Departments
- **Patient**: Patient registration and management
- **Clinical**: Staff, Encounters, Appointments
- **Billing**: Invoices, Payments, Service Catalog
- **Audit**: Complete audit trail for all operations

## Technology Stack

- .NET 8
- Entity Framework Core 8
- PostgreSQL (via Npgsql)
- Supabase (Database provider)
- AutoMapper
- Swagger/OpenAPI

## Getting Started

### Prerequisites

- .NET 8 SDK
- PostgreSQL database (Supabase)
- Visual Studio 2022 or VS Code

### Database Setup

1. Execute the SQL schema in your Supabase SQL Editor:
   ```sql
   -- See database/schema.sql
   ```

2. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Host=your-supabase-host;Database=postgres;Username=postgres;Password=your-password;Port=5432"
   }
   ```

### Running the Application

```bash
# Build the solution
dotnet build

# Run the API
dotnet run --project src/HospitalAPI.API

# Navigate to Swagger UI
https://localhost:5001/swagger
```

## API Endpoints

### Patients
- `GET /api/patients` - Get all patients
- `GET /api/patients/{id}` - Get patient by ID
- `GET /api/patients/mrn/{mrn}` - Get patient by MRN
- `GET /api/patients/search` - Search patients
- `POST /api/patients` - Create new patient
- `PUT /api/patients/{id}` - Update patient
- `DELETE /api/patients/{id}` - Soft delete patient

## Project Structure

```
Hospital_api/
├── src/
│   ├── HospitalAPI.Domain/
│   │   ├── Entities/
│   │   │   ├── Common/
│   │   │   ├── IAM/
│   │   │   ├── Organization/
│   │   │   ├── Patient/
│   │   │   ├── Clinical/
│   │   │   ├── Billing/
│   │   │   └── Audit/
│   │   └── Interfaces/
│   │       └── Repositories/
│   ├── HospitalAPI.Application/
│   │   ├── DTOs/
│   │   ├── Services/
│   │   ├── Interfaces/
│   │   └── Mappings/
│   ├── HospitalAPI.Infrastructure/
│   │   ├── Data/
│   │   │   └── Configurations/
│   │   └── Repositories/
│   ├── HospitalAPI.API/
│   │   ├── Controllers/
│   │   └── Middleware/
│   └── HospitalAPI.Common/
│       ├── Constants/
│       ├── Exceptions/
│       ├── Helpers/
│       └── Models/
└── HospitalAPI.sln
```

## Development

### Adding a New Entity

1. Create entity in `Domain/Entities`
2. Create repository interface in `Domain/Interfaces/Repositories`
3. Create DTOs in `Application/DTOs`
4. Create service interface in `Application/Interfaces/Services`
5. Implement repository in `Infrastructure/Repositories`
6. Implement service in `Application/Services`
7. Add EF Core configuration in `Infrastructure/Data/Configurations`
8. Create controller in `API/Controllers`
9. Register services in `Program.cs`

## License

MIT License
