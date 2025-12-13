# Hospital API - Quick Start Guide

## Step 1: Execute Database Schema

1. Open your Supabase Dashboard: https://supabase.com/dashboard
2. Navigate to your project
3. Go to **SQL Editor**
4. Copy the entire contents of `database/schema.sql`
5. Paste into the SQL Editor
6. Click **Run** or press `Ctrl+Enter`

You should see "Success. No rows returned" - this means all tables were created successfully.

## Step 2: Verify Connection String

The connection string in `src/HospitalAPI.API/appsettings.json` is already configured:
```
Host=aws-1-ap-south-1.pooler.supabase.com
Database=postgres
Username=postgres.tpsqdiowlfuxtrwxkowm
Password=Ushmachintusunnybunnysai@cousins5
Port=5432
```

## Step 3: Run the API

Open a terminal in the project root and run:
```bash
dotnet run --project src/HospitalAPI.API/HospitalAPI.API.csproj
```

The API will start on:
- HTTP: http://localhost:5000
- HTTPS: https://localhost:5001

## Step 4: Access Swagger

Open your browser and navigate to:
```
https://localhost:5001/swagger
```

or

```
http://localhost:5000/swagger
```

## Step 5: Test the API

In Swagger, you can test the Patients API:

1. **GET /api/patients** - List all patients (requires tenantId query parameter)
2. **POST /api/patients** - Create a new patient
3. **GET /api/patients/{id}** - Get patient by ID
4. **GET /api/patients/mrn/{mrn}** - Get patient by MRN
5. **GET /api/patients/search** - Search patients

## Troubleshooting

### If you get "TypeLoadException":
This usually means there's a package version mismatch. Run:
```bash
dotnet clean
dotnet restore
dotnet build
```

### If you get database connection errors:
1. Verify your Supabase project is active
2. Check that the schema.sql has been executed
3. Verify the connection string is correct
4. Make sure your IP is allowed in Supabase (usually auto-allowed)

### If Swagger doesn't load:
Try the HTTP version instead of HTTPS: `http://localhost:5000/swagger`

## Next Steps

Once the API is running successfully:

1. **Add more controllers** - Use PatientsController as a template
2. **Implement authentication** - Add Supabase JWT authentication
3. **Add authorization** - Implement permission-based access control
4. **Create seed data** - Add initial roles and permissions
5. **Add validation** - Use FluentValidation for request validation
6. **Implement audit logging** - Track all data changes
7. **Add unit tests** - Test your services and controllers

## Sample API Calls

### Create a Patient
```json
POST /api/patients?tenantId=00000000-0000-0000-0000-000000000001
{
  "firstName": "John",
  "lastName": "Doe",
  "phone": "+1234567890"
}
```

### Search Patients
```
GET /api/patients/search?tenantId=00000000-0000-0000-0000-000000000001&searchTerm=John
```

Happy coding! 🚀
