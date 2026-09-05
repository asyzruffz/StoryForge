# Domain and Persistence

Domain entities are mutable classes with safe defaults. Use their existing `New(...)` factory when creating instances. Use lightweight entry records such as `CharacterEntry` for lists rather than exposing whole entities unnecessarily.

- Model IDs as `readonly record struct` types backed by GUID strings, following `CharacterId` or `BookId`; use `TypedId.New()` and `TypedId.From()`.
- Use `Result`/`Result<T>` for expected errors and `Option<T>` for optional values. Reserve `Throw` and exceptions for invariants or exceptional failures.
- Persist project data through scoped `IDataSession`; use `IApplicationDataSession` for application-level metadata. Repositories are exposed from those session interfaces.
- SQLite is the production store; the in-memory implementation supports isolated/testing scenarios.
- EF Core schema is created through `EnsureCreatedAsync`, not migrations.

When adding a persisted entity, update the Core entity and repository contract, SQLite repository, `ProjectDbContext` `DbSet`, `IEntityTypeConfiguration<T>`, and any in-memory implementation that must support the feature. Configure typed-ID conversions and owned/value types consistently with nearby configurations.
