# animalreviewapi

Animal Review API

## Overview

This is a RESTful API built with ASP.NET Core (.NET 6) for managing animal reviews. It allows you to manage animals, categories, countries, owners, reviewers, and reviews.

## Features

- CRUD operations for animals, categories, owners, reviewers, and reviews
- Entity Framework Core with SQL Server
- AutoMapper for DTO mapping
- Swagger/OpenAPI documentation

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [Git](https://git-scm.com/)

### Setup

1. **Clone the repository:**

```bash
git clone https://github.com/yourusername/animalreviewapi.git
cd animalreviewapi
```

2. **Restore dependencies:**

```bash
dotnet restore
```

3. **Update the connection string:**
   - Edit `appsettings.json` and set your SQL Server connection string under `DefaultConnection`.

4. **Apply migrations and seed data (optional):**

```bash
dotnet ef database update
dotnet run
```

5. **Run the application:**

```bash
dotnet run
```

6. **Access Swagger UI:**
   - Navigate to `https://localhost:<port>/swagger` in your browser.

## Project Structure

- `Controllers/` - API controllers
- `Repository/` - Data access logic
- `Interfaces/` - Repository interfaces
- `Models/` - Entity models
- `Dto/` - Data transfer objects
- `Data/` - Database context and seeding

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License

[MIT](LICENSE)
