using StoryForge.Core.Storage;
using StoryForge.Core.Storage.Repositories;
using StoryForge.Infrastructure.Database.SQLite;
using StoryForge.Infrastructure.Database.SQLite.Repositories;

namespace StoryForge.Infrastructure.Database;

public class ApplicationDataSession : IApplicationDataSession
{
    private readonly ApplicationDbContext context;

    public ApplicationDataSession(ApplicationDbContext context)
    {
        this.context = context;

        Projects = new ProjectRepository(context);
    }

    public IProjectRepository Projects { get; init; }

    public Task<bool> EnsureCreatedAsync(CancellationToken ct) =>
        context.Database.EnsureCreatedAsync(ct);

    public Task<int> SaveAsync(CancellationToken ct) =>
        context.SaveChangesAsync(ct);

    public ValueTask DisposeAsync() => context.DisposeAsync();
}
