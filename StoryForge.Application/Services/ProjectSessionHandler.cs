using Microsoft.Extensions.DependencyInjection;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Core.Services;

public class ProjectSessionHandler : IProjectSessionHandler
{
    private readonly IServiceScopeFactory scopeFactory;
    private readonly IApplicationDataSession appData;

    public bool IsActive { get; private set; } = false;

    public string? CurrentProject { get; private set; }

    IServiceScope? projectScope;

    public ProjectSessionHandler(IServiceScopeFactory serviceScopeFactory, IApplicationDataSession appDataSession)
    {
        scopeFactory = serviceScopeFactory;
        appData = appDataSession;
    }

    public Result StartSession(Project project, bool newlyCreated = false)
    {
        try
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

            if (newlyCreated)
            {
                CreateNew(project, dataSession);
            }

            IsActive = true;
            CurrentProject = project.FilePath;
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public void StopSession()
    {
        IsActive = false;
        projectScope?.Dispose();
        projectScope = null;
        CurrentProject = null;
    }

    void CreateNew(Project project, IDataSession dataSession)
    {
        appData.Projects.Create(project);
        dataSession.Books.Update(new Book
        {
            Id = BookId.New(),
            Title = Path.GetFileNameWithoutExtension(project.FilePath),
            Extra = BookSummary.New()
        });
        dataSession.Authors.Update(new Author { Id = AuthorId.New() });

        appData.Save();
        dataSession.Save();
    }

    public void Dispose()
    {
        StopSession();
    }
}
