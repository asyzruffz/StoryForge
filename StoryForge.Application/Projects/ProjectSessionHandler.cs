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

    public Task<Result> LoadSession(string filePath, CancellationToken ct = default)
    {
        return SetupSession(filePath, async (dataSession, ct) =>
        {
            var projectName = dataSession.Meta
                .Get(ProjectMeta.Name)
                .Or(Path.GetFileNameWithoutExtension(filePath));

            var project = new Project { FilePath = filePath, Name = projectName };

            await RegisterProject(project, ct)
                .ConfigureAwait(false);
        }, ct);
    }

    public Task<Result> StartSession(Project project, CancellationToken ct = default)
    {
        return SetupSession(project.FilePath, async (dataSession, ct) =>
        {
            await RegisterProject(project, ct)
                .ConfigureAwait(false);
            await CreateNew(project, dataSession, ct)
                .ConfigureAwait(false);
        }, ct);
    }

    async Task<Result> SetupSession(string filePath, Func<IDataSession, CancellationToken, Task> onProjectNotRegistered, CancellationToken ct)
    {
        try
        {
            if (IsActive)
            {
                await StopSession().ConfigureAwait(false);
            }

            projectScope = scopeFactory.CreateAsyncScope();
            var provider = projectScope!.Value.ServiceProvider;

            CurrentProject = filePath;

            var dataSession = provider.GetRequiredService<IDataSession>();
            await dataSession.EnsureCreatedAsync(ct).ConfigureAwait(false);

            var projectResult = appData.Projects.GetById(filePath);

            await projectResult.MatchAsync(
                onSuccess: async (project, ct) =>
                {
                    project.SetActive();
                    appData.Projects.Update(project);
                    await appData.SaveAsync(ct).ConfigureAwait(false);
                },
                onFailure: (_, ct) => onProjectNotRegistered(dataSession, ct),
                ct)
                .ConfigureAwait(false);

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

    async Task RegisterProject(Project project, CancellationToken ct)
    {
        appData.Projects.Create(project);
        await appData.SaveAsync(ct).ConfigureAwait(false);
    }

    async Task CreateNew(Project project, IDataSession dataSession, CancellationToken ct)
    {
        dataSession.Meta.Set(ProjectMeta.Name, project.Name);
        dataSession.Books.Update(new Book
        {
            Id = BookId.New(),
            Title = Path.GetFileNameWithoutExtension(project.FilePath),
            Extra = BookSummary.New()
        });
        dataSession.Authors.Update(new Author { Id = AuthorId.New() });

        await dataSession.SaveAsync(ct).ConfigureAwait(false);
    }

    public async ValueTask DisposeAsync()
    {
        await StopSession().ConfigureAwait(false);
    }
}
