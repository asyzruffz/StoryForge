using StoryForge.Core.Data;

namespace StoryForge.Infrastructure.Database;

public class ApplicationDbContext
{
    public List<Chapter> Chapters { get; } = [];

    public int SaveChanges() => 0;
    public void Dispose() { }
}
