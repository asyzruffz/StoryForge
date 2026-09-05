using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StoryForge.Core.Projects;
using StoryForge.Infrastructure.Database.SQLite;

namespace StoryForge.Infrastructure.Database;

public class ProjectDbFactory
{
    private readonly ILogger<ProjectDbFactory> logger;

    public ProjectDbFactory(ILogger<ProjectDbFactory> logger)
    {
        this.logger = logger;
    }

    public ProjectDbContext CreateDbContext(IServiceProvider provider)
    {
        var sessionHandler = provider.GetRequiredService<IProjectSessionHandler>();
        if (sessionHandler.CurrentProject is null)
            logger.LogError("ProjectDbContext instantiated while CurrentProject is null!");

        var filePath = sessionHandler.CurrentProject;
        var dbPath = string.IsNullOrWhiteSpace(filePath) ? ":memory:" : filePath;

        var options = new DbContextOptionsBuilder<ProjectDbContext>()
            .UseSqlite($"Data Source={dbPath}")
            .Options;
        return new ProjectDbContext(options);
    }
}
