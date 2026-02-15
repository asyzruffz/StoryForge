using StoryForge.Core.Services;

namespace StoryForge.Infrastructure.Database;

public class ProjectScopeContext : IProjectScopeContext
{
    public string? ProjectFilePath { get; set; }
}
