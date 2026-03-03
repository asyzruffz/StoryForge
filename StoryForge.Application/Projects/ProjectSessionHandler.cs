using Microsoft.Extensions.DependencyInjection;
using StoryForge.Core.Data;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public class ProjectSessionHandler : IProjectSessionHandler
{
    private readonly IServiceScopeFactory scopeFactory;
    private readonly IApplicationDataSession appData;

    public bool IsActive { get; private set; } = false;

    public string? CurrentProject { get; private set; }

    AsyncServiceScope? projectScope;

    public ProjectSessionHandler(IServiceScopeFactory serviceScopeFactory, IApplicationDataSession appDataSession)
    {
        scopeFactory = serviceScopeFactory;
        appData = appDataSession;
    }

    public async Task<Result> StartSession(Project project, bool newlyCreated = false, CancellationToken ct = default)
    {
        try
        {
            if (IsActive)
            {
                await StopSession().ConfigureAwait(false);
            }

            projectScope = scopeFactory.CreateAsyncScope();
            var provider = projectScope!.Value.ServiceProvider;

            CurrentProject = project.FilePath;

            var dataSession = provider.GetRequiredService<IDataSession>();
            await dataSession.EnsureCreatedAsync(ct).ConfigureAwait(false);

            if (newlyCreated)
            {
                await CreateNew(project, dataSession, ct)
                    .ConfigureAwait(false);
            }

            IsActive = true;
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public async Task StopSession()
    {
        await (projectScope?.DisposeAsync() ?? ValueTask.CompletedTask)
            .ConfigureAwait(false);
        IsActive = false;
        projectScope = null;
        CurrentProject = null;
    }

    async Task CreateNew(Project project, IDataSession dataSession, CancellationToken ct)
    {
        appData.Projects.Create(project);
        dataSession.Books.Update(new Book
        {
            Id = BookId.New(),
            Title = Path.GetFileNameWithoutExtension(project.FilePath),
            Extra = BookSummary.New()
        });
        dataSession.Authors.Update(new Author { Id = AuthorId.New() });

        await appData.SaveAsync(ct).ConfigureAwait(false);
        await dataSession.SaveAsync(ct).ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        await StopSession().ConfigureAwait(false);
    }
}
