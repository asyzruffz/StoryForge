# WARP.md

This file provides guidance to WARP (warp.dev) when working with code in this repository.

## Project Overview

StoryForge is a .NET 8.0 console application that uses AI to generate and manage story chapters. It's built with Clean Architecture principles and integrates with OpenAI-compatible APIs (including local vLLM instances) for text generation.

## Build and Run Commands

### Build the solution
```powershell
dotnet build StoryForge.sln
```

### Run the simulator (main application)
```powershell
dotnet run --project StoryForge.Simulator
```

### Build and run with Docker Compose
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

**StoryForge.Simulator** - Presentation layer (Console UI)
- Entry point with `Program.cs` using Microsoft.Extensions.Hosting
- Interactive command-line interface with command processor
- Commands registered as keyed services (e.g., `chapter`, `test`, `prompt`, `version`, `clear`)
- Commands use MediatR to dispatch operations to the application layer

### Key Architectural Patterns

**Dependency Flow**: Simulator → Infrastructure → Application → Core (dependencies point inward)

**Command Pattern**: User input is parsed into `CommandData` and routed to registered `ICommand` implementations via keyed DI services.

**Result Pattern**: All operations return `Result` or `Result<T>` for explicit error handling without exceptions. Use `.Then()` for chaining and `.OnError()` for error handling.

**Repository Pattern**: Data access is abstracted through `IDataSession` which provides repository instances. Currently uses in-memory `ApplicationDbContext`.

**Linked List Structure**: `Chapter` entities maintain bidirectional links (Prev/Next) for story navigation.

## Configuration

### appsettings.json

Located in `StoryForge.Simulator/appsettings.json`:

- **OpenAI.Key**: API key (use "EMPTY" for local vLLM)
- **OpenAI.Url**: API endpoint (default: `http://storyforge.ai:8000/v1` for Docker, change to `http://localhost:8000/v1` for local)
- **OpenAI.Model**: Model identifier (e.g., `mistralai/Mistral-7B-v0.1` or `gpt2`)

### Docker Configuration

`StoryForge.Containers/docker-compose.yml` sets up:
- `storyforge.simulator`: The console application
- `storyforge.ai`: vLLM OpenAI-compatible server with GPU support

Update `HUGGING_FACE_HUB_TOKEN` in docker-compose.yml to download gated models.

## Application Commands

When running the simulator, use these interactive commands:

### Chapter Management
- `chapter list` - List all chapters with IDs
- `chapter add <title>` - Add a new chapter
- `chapter delete <chapter-id>` - Delete a chapter by ID (first 8 chars of ID work)
- `chapter link <chapter-id-1> <chapter-id-2>` - Link two chapters in sequence

### AI Generation
- `test gen <prompt>` - Generate text using AI with a custom prompt
- `prompt <prompt-text>` - Generate text using the AI service

### Utility
- `version` - Show application version
- `clear` - Clear the console
- `exit` - Exit the application

## Development Guidelines

### Adding New Operations

1. Create operation record in `StoryForge.Application/<Feature>/` implementing `IOperation` or `IOperation<TResponse>`
2. Create internal handler class implementing `IOperationHandler<TOperation>` or `IOperationHandler<TOperation, TResponse>`
3. MediatR will auto-register handlers via assembly scanning (configured in `StoryForgeApplication.cs`)
4. Operations are dispatched via `ISender.Send()` from commands or other operations

### Adding New Commands

1. Create command class in `StoryForge.Simulator/Commands/` inheriting from `CommandBase`
2. Define const `Name` property for command name
3. Override `Action` property with command logic
4. Register in `CommandServiceExtension.AddCommandService()` as a keyed service
5. Commands receive `CommandData` with parsed parameters and use MediatR to send operations

### Error Handling

Use the `Result<T>` pattern throughout:
- Return `Result.Ok()` or `Result<T>.Ok(value)` for success
- Return `Result.Fail(errorMessage)` or `Result<T>.Fail(errorMessage)` for errors
- Chain operations with `.Then()` 
- Handle errors with `.OnError(action)`
- Extract values with `.Or(defaultValue)` or `.Match(onSuccess, onFailure)`

### Working with Chapters

Chapters use a doubly-linked list structure:
- Use `Chapter.SetNext()` to insert a chapter in the sequence (handles unlinking automatically)
- Each chapter has `Prev` and `Next` properties (nullable)
- Chapter IDs are strongly-typed via `ChapterId` record struct
- Access chapters through `IDataSession.Chapters` repository

## Technology Stack

- **.NET 8.0** with nullable reference types enabled
- **MediatR 12.4.0** for CQRS/mediator pattern
- **OpenAI 2.0.0-beta.8** SDK for AI integration
- **Microsoft.Extensions.Hosting** for application lifetime management
- **Docker** with vLLM for local AI inference
