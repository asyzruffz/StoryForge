# Coding Conventions

- Target `net10.0` with nullable reference types and implicit usings enabled.
- Use PascalCase for namespaces, types, and members; camelCase for locals and private fields; and `UPPER_SNAKE_CASE` for constants.
- Keep namespaces feature-oriented, such as `StoryForge.Application.Characters.Operations`.
- Use sealed records for operations and `internal sealed` types for handlers unless extension is intentional.
- Name operations `[Action][Entity]Operation`; keep a handler inline or name it `[Action][Entity]OperationHandler`.
- Use readonly injected fields without an underscore where that is the surrounding convention.
- Match nearby formatting and `ConfigureAwait(false)` usage. Avoid unrelated refactors.

Logging is minimal. When it is needed, inject `ILogger<T>`, use structured context, and select an appropriate level. Do not log secrets, API keys, or sensitive project content.
