# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Development Commands

### Backend (IssueOtter/)
```bash
# Navigate to the backend directory
cd IssueOtter

# Restore dependencies
dotnet restore

# Build the project
dotnet build

# Run in development mode
dotnet run

# Run with specific profile
dotnet run --launch-profile "http"
dotnet run --launch-profile "https"
```

### Frontend (frontend/)
```bash
# Navigate to the frontend directory
cd ../frontend

# Install dependencies
npm install

# Run development server
npm run dev

# Build for production
npm run build

# Lint code
npm run lint

# Preview production build
npm run preview
```

### Database Operations
```bash
# From IssueOtter/ directory
# Add new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

### Docker
```bash
# Build Docker image (from IssueOtter/ directory)
docker build -t issueotter .

# Run container
docker run -p 8080:8080 issueotter
```

## Architecture

### Project Structure
**Backend (IssueOtter/):**
- **Api/Controllers/**: REST API controllers for Project, Issue, Comment, and User entities
- **Core/**: Business logic layer
  - **Entities/**: Domain models (Project, Issue, Comment, User)
  - **Services/**: Business logic services implementing interfaces
  - **Interfaces/**: Service contracts
  - **Dtos/**: Data transfer objects for API responses
  - **Mappers/**: Entity to DTO mapping logic
- **Infrastructure/**: Data access layer
  - **Repositories/**: Entity Framework repositories implementing data access
  - **Migrations/**: Database schema migrations

**Frontend (frontend/):**
- **src/Api/**: HTTP client services for backend communication
- **src/Components/**: Reusable React components
- **src/Pages/**: Page-level React components
- **src/Routes/**: React Router configuration
- **src/Models/**: TypeScript interfaces and types

### Technology Stack
**Backend:**
- **Framework**: ASP.NET Core 8 Web API
- **Database**: MySQL with Entity Framework Core 8
- **Authentication**: Auth0 (production) / Dev handler (development)
- **API Documentation**: Swagger/OpenAPI

**Frontend:**
- **Build Tool**: Vite
- **Framework**: React 18 with TypeScript
- **HTTP Client**: Axios
- **Form Handling**: React Hook Form with Yup validation
- **Styling**: TailwindCSS with DaisyUI components
- **Authentication**: Auth0 React SDK
- **Routing**: React Router 6

### Authentication
**Backend**: Different strategies based on environment:
- **Development**: `DevAuthHandler` for local development
- **Production**: Auth0 JWT Bearer authentication

**Frontend**: Auth0 React SDK for user authentication and authorization

### Database Configuration
MySQL database with connection string from `MySqlConnectionString` configuration key. Database context is `ApplicationDbContext`.

### API Endpoints
REST endpoints for Projects, Issues, Comments, and Users with full CRUD operations.

### Development Environment
**Backend:**
- Development URL: `http://localhost:5285`
- HTTPS URL: `https://localhost:7180`
- Swagger UI: `/swagger` endpoint

**Frontend:**
- Development server runs on Vite's default port (typically `http://localhost:5173`)
- Hot reload enabled for development