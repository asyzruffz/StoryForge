# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Project Overview

StoryForge is a .NET 8.0 desktop application that uses AI to generate and manage stories. It's built with Clean Architecture principles and features a Blazor-based UI using Photino for native desktop rendering. The application integrates with OpenAI-compatible APIs (including local vLLM instances) for text generation.

## Build and Run Commands

### Build the solution
```powershell
dotnet build StoryForge.sln
```

### Run the desktop application
```powershell
dotnet run --project StoryForge.Desktop
```

### Build and run vLLM backend with Docker Compose
```powershell
docker-compose -f StoryForge.Containers/docker-compose.yml up --build
```

Note: The Docker setup includes a vLLM service for local AI inference with GPU support.

## Architecture

### Clean Architecture Layers

The solution follows Clean Architecture with four main projects:

**StoryForge.Core** - Domain layer
- Contains domain entities (`Chapter`, `Title`, `ContentSection`)
- Defines repository interfaces (`IChapterRepository`, `IRepository`)
- Defines service interfaces (`IAIService`, `IDataSession`, `IRandomService`, `ITemporaryStorage`)
- Contains utility types (`Result<T>`, `Option<T>`) for functional error handling
- Uses strongly-typed IDs via `TypedId` pattern (e.g., `ChapterId`)

**StoryForge.Application** - Application layer
- Uses **MediatR** for CQRS pattern with `IOperation` and `IOperationHandler` abstractions
- Operations are located in feature folders (e.g., `Chapters/`, `AIGeneration/`)
- Each operation is a request-response pair following the Result pattern
- Service implementations that don't depend on external infrastructure

**StoryForge.Infrastructure** - Infrastructure layer
- Database implementation using `ApplicationDbContext` (currently in-memory, not EF Core)
- OpenAI integration via `OpenAIClient` (works with OpenAI API or vLLM endpoints)
- Repository implementations in `Database/Repositories/`
- Configured via `appsettings.json` with sections for `ConnectionStrings` and `OpenAI`

**StoryForge.Desktop** - Presentation layer (Desktop UI)
- Native desktop application using **Photino.Blazor** for cross-platform rendering
- **MudBlazor** component library for Material Design UI
- Razor components organized in `UI/Pages/` (e.g., `Home`, `Characters`, `Plots`, `World`, `Outline`)
- `MainLayout.razor` provides the application shell with navigation drawer and app bar
- Services configured in `Extensions.cs` with MediatR for operation dispatch

### Key Architectural Patterns

**Dependency Flow**: Desktop → Infrastructure → Application → Core (dependencies point inward)

**Component-Based UI**: Blazor components communicate via parameters and cascading values, using MediatR to dispatch operations to the application layer.

**Result Pattern**: All operations return `Result` or `Result<T>` for explicit error handling without exceptions. Use `.Then()` for chaining and `.OnError()` for error handling.

**Repository Pattern**: Data access is abstracted through `IDataSession` which provides repository instances. Currently uses in-memory `ApplicationDbContext`.

## Configuration

### Docker Configuration

`StoryForge.Containers/docker-compose.yml` sets up:
- `storyforge.ai`: vLLM OpenAI-compatible server with GPU support

Update `HUGGING_FACE_HUB_TOKEN` in docker-compose.yml to download gated models.

## Application Pages

The desktop application provides the following pages:

- **Home** - Dashboard and overview
- **General / BookInfo / AuthorInfo** - Story and author metadata
- **Characters / CharacterInfo** - Character management and details
- **World / LocationInfo** - World-building and locations
- **Plots / PlotInfo** - Plot threads and details
- **Outline / Summary** - Story structure and summaries

## Development Guidelines

### Adding New Operations

1. Create operation record in `StoryForge.Application/<Feature>/` implementing `IOperation` or `IOperation<TResponse>`
2. Create internal handler class implementing `IOperationHandler<TOperation>` or `IOperationHandler<TOperation, TResponse>`
3. MediatR will auto-register handlers via assembly scanning (configured in `Extensions.cs` from `StoryForge.Desktop`)
4. Operations are dispatched via `ISender.Send()` from commands or other operations

### Adding New Pages

1. Create a new `.razor` file in `StoryForge.Desktop/UI/Pages/`
2. Add the `@page "/route"` directive at the top
3. Inject required services (e.g., `@inject ISender sender` for MediatR)
4. Use MudBlazor components for UI elements
5. Dispatch operations via `sender.Send()` for application layer communication

### Error Handling

Use the `Result<T>` pattern throughout:
- Return `Result.Ok()` or `Result<T>.Ok(value)` for success
- Return `Result.Fail(errorMessage)` or `Result<T>.Fail(errorMessage)` for errors
- Chain operations with `.Then()` 
- Handle errors with `.OnError(action)`
- Extract values with `.Or(defaultValue)` or `.Match(onSuccess, onFailure)`

## Technology Stack

- **.NET 8.0** with nullable reference types enabled
- **Photino.Blazor 4.0** for native desktop rendering with Blazor
- **MudBlazor 8.15** for Material Design UI components
- **MediatR 12.4.0** for CQRS/mediator pattern
- **OpenAI 2.0.0-beta.8** SDK for AI integration
- **Docker** with vLLM for local AI inference
