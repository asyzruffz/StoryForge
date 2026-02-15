using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Services;
using StoryForge.Infrastructure.Database.SQLite;

namespace StoryForge.Infrastructure.Database;

public class DataSessionFactory : IDataSessionFactory
{
    public DataSessionFactory() { }

    public IDataSession CreateSession(string filePath)
    {
        var dbPath = string.IsNullOrWhiteSpace(filePath) ? ":memory:" : filePath;

        var options = new DbContextOptionsBuilder<ProjectDbContext>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;
        return new DataSession(new ProjectDbContext(options));
    }
}
