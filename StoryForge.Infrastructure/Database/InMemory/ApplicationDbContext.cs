using StoryForge.Core.Data;

namespace StoryForge.Infrastructure.Database.InMemory;

public class ApplicationDbContext
{
    public List<Summary> Summaries { get; } = [];
    public List<Character> Characters { get; } = [];
    public List<Chapter> Chapters { get; } = [];

    public int SaveChanges() => 0;
    public void Dispose() { }
}
