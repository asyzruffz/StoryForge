using Keystone;
using Microsoft.Extensions.DependencyInjection;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;

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

    public ValueTask<Result> LoadSession(string filePath, CancellationToken ct = default)
    {
        return SetupSession(filePath, async (dataSession, ct) =>
        {
            var projectName = dataSession.Meta
                .Get(ProjectMeta.Name)
                .Or(Path.GetFileNameWithoutExtension(filePath));

            var project = new Project { FilePath = filePath, Name = projectName };

            await project.RegisterToAppAsync(appData, ct)
                .ConfigureAwait(false);
        }, ct);
    }

    public ValueTask<Result> StartSession(Project project, CancellationToken ct = default)
    {
        return SetupSession(project.FilePath, async (dataSession, ct) =>
        {
            await project.RegisterToAppAsync(appData, ct)
                .ConfigureAwait(false);
            await project.InitializeAsync(dataSession, ct)
                .ConfigureAwait(false);
        }, ct);
    }

    async ValueTask<Result> SetupSession(string filePath, Func<IDataSession, CancellationToken, Task> onProjectNotRegistered, CancellationToken ct)
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

            var projectResult = appData.Projects.GetById(filePath)
            .ToResult($"Couldn't find project at {filePath}");

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

    public async ValueTask StopSession()
    {
        await (projectScope?.DisposeAsync() ?? ValueTask.CompletedTask)
            .ConfigureAwait(false);
        IsActive = false;
        projectScope = null;
        CurrentProject = null;
    }

    public async ValueTask DisposeAsync()
    {
        await StopSession().ConfigureAwait(false);
    }
}
