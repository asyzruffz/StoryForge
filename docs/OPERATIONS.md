# Operations and Dependency Injection

Use Keystone, not MediatR, for new application work.

- Define commands as `sealed record ... : IOperation` and queries as `sealed record ... : IOperation<TResponse>`.
- Place one matching `internal sealed` `IOperationHandler` beside its operation in the appropriate Application feature folder.
- Handlers are discovered by the Application assembly scan; do not manually register them unless scanning cannot apply.
- Inject `ISender` into Razor components and dispatch operations through `sender.Send(...)`.
- Return `ValueTask<Result>` or `ValueTask<Result<T>>`; pass cancellation tokens through I/O and save with `await data.SaveAsync(ct)`.
- Validate expected failure paths early and return `Result.Fail(...)`. Use `Result` composition (`Then`, `ThenAsync`, `Match`) rather than exceptions for normal control flow.

`IProjectSessionHandler` owns the active project scope. `IDataSession` and its `ProjectDbContext` are scoped to that project; never retain them, their entities, or other scoped services in singletons. `ProjectDbFactory` uses the active project path and falls back to in-memory storage only when no project is active.

For a new feature, add the domain contract/entity first, then its repository implementation, operation/handler, DI registration when needed, and UI.
