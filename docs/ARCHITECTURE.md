# Architecture

StoryForge is a .NET 10 clean-architecture desktop application for story development.

- `Keystone` provides `Result`, `Option`, `Throw`, and operation dispatching.
- `StoryForge.Core` holds domain types and abstraction contracts; it must not depend on UI, EF Core, or external providers.
- `StoryForge.Application` implements feature use cases through operations and handlers.
- `StoryForge.Infrastructure` implements persistence and provider adapters.
- `StoryForge.Presentation` is the active Razor/BlazorBlueprint UI library.
- `StoryForge.Desktop` is the Photino.Blazor host and production composition root.
- `StoryForge.MudPresentation` is an unreferenced MudBlazor migration reference. `StoryForge.Simulator` has separate, older wiring.

Prefer dependencies toward the core: UI/host -> Application -> Core. Infrastructure implements Core/Application contracts and is registered only at composition roots.

`StoryForge.Desktop/Program.cs` and `Extensions.cs` register Keystone, scan the Application assembly for handlers, configure storage and presentation services, initialize the application database, and start Photino.
