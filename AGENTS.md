# Repository Guidelines

## Project Structure & Module Organization

MealMate is organized as a .NET 10 Clean Architecture solution under `backend/`:

- `src/MealMate.Domain/` contains core entities, value objects, and domain rules. Keep it independent of other projects.
- `src/MealMate.Application/` contains use cases, interfaces, DTOs, and validation. It may reference Domain only.
- `src/MealMate.Infrastructure/` implements persistence and external-service interfaces. It references Application and Domain.
- `src/MealMate.Api/` is the ASP.NET Core entry point, HTTP pipeline, endpoints, and dependency registration.
- `backend/MealMate.slnx` groups the backend projects. No frontend or test project is currently present.

## Build, Test, and Development Commands

Run commands from the repository root:

```bash
dotnet restore backend/MealMate.slnx
dotnet build backend/MealMate.slnx
dotnet run --project backend/src/MealMate.Api
dotnet test backend/MealMate.slnx
dotnet format backend/MealMate.slnx --verify-no-changes
```

`restore` downloads dependencies, `build` compiles all projects, and `run` starts the API using its launch settings. `test` will execute test projects once they are added. Use `dotnet format` before submitting changes.

## Coding Style & Naming Conventions

Use standard C# conventions: four-space indentation, file-scoped namespaces where practical, PascalCase for types and public members, camelCase for locals and parameters, and an `I` prefix for interfaces. Nullable reference types and implicit usings are enabled; address nullable warnings instead of suppressing them. Keep dependencies flowing inward and place registrations or framework-specific code in Api or Infrastructure.

## Testing Guidelines

Add tests under `backend/tests/` with projects named `MealMate.<Layer>.Tests`. Name test files after the subject, such as `CreateOrderHandlerTests.cs`, and use descriptive methods such as `Handle_ReturnsFailure_WhenRestaurantIsClosed`. Prioritize domain rules and application use cases; add integration tests for API and persistence behavior. Run `dotnet test backend/MealMate.slnx` before opening a pull request.

## Commit & Pull Request Guidelines

History currently contains only `Initial commit`, so no established convention exists. Use concise, imperative commit subjects, for example `Add menu item validation`. Keep commits focused. Pull requests should explain the change and validation performed, link related issues, call out configuration or migration steps, and include request/response examples for API behavior changes.

## Security & Configuration

Do not commit secrets to `appsettings*.json`. Use environment variables or .NET user secrets for local credentials. Document newly required settings in the pull request and provide safe placeholder values where appropriate.
