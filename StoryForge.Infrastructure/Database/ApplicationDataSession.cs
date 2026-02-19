using StoryForge.Core.Storage;
using StoryForge.Core.Storage.Repositories;
using StoryForge.Infrastructure.Database.SQLite;
using StoryForge.Infrastructure.Database.SQLite.Repositories;

namespace StoryForge.Infrastructure.Database;

public class ApplicationDataSession : IApplicationDataSession, IDisposable
{
    private readonly ApplicationDbContext context;

    public ApplicationDataSession(ApplicationDbContext context)
    {
        this.context = context;

        Projects = new ProjectRepository(context);
    }

    public IProjectRepository Projects { get; init; }

    public bool EnsureCreated() => context.Database.EnsureCreated();
    public int Save() => context.SaveChanges();
    public void Dispose() => context.Dispose();
}
