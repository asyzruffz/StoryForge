using Microsoft.Extensions.DependencyInjection;
using StoryForge.Core.Data;
using StoryForge.Core.Utils;

namespace StoryForge.Core.Services;

public class ProjectSessionHandler : IProjectSessionHandler
{
    private readonly IServiceScopeFactory scopeFactory;

    public bool IsActive { get; private set; } = false;

    public ProjectId? CurrentProject { get; private set; }

    IServiceScope? projectScope;

    public ProjectSessionHandler(IServiceScopeFactory serviceScopeFactory)
    {
        scopeFactory = serviceScopeFactory;
    }

    public Result StartSession(Project project)
    {
        if (IsActive)
        {
            StopSession();
        }

        projectScope = scopeFactory.CreateScope();
        var provider = projectScope.ServiceProvider;

        var scopeCtx = provider.GetRequiredService<IProjectScopeContext>();
        scopeCtx.ProjectFilePath = project.FilePath;

        var dataSession = provider.GetRequiredService<IDataSession>();
        dataSession.EnsureCreated();

        dataSession.Summaries.Create(project.Book.Extra.Summary);
        dataSession.Save();

        IsActive = true;
        CurrentProject = project.Id;
        return Result.Ok();
    }

    public void StopSession()
    {
        IsActive = false;
        projectScope?.Dispose();
        projectScope = null;
        CurrentProject = null;
    }

    public void Dispose()
    {
        StopSession();
    }
}
