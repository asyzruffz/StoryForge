using StoryForge.Core.Projects;

namespace StoryForge.Infrastructure.Database.InMemory;

public class ApplicationDbContext
{
    public List<Project> Projects { get; } = [];

    public int SaveChanges() => 0;
    public void Dispose() { }
}
