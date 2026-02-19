using StoryForge.Application.Abstractions;
using StoryForge.Core.Services;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record OpenProjectPathOperation(string FilePath) : IOperation;

internal sealed class OpenProjectPathOperationHandler : IOperationHandler<OpenProjectPathOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IApplicationDataSession appData;

    public OpenProjectPathOperationHandler(IProjectSessionHandler projectSessionHandler, IApplicationDataSession appDataSession)
    {
        projectSession = projectSessionHandler;
        appData = appDataSession;
    }

    public async Task<Result> Handle(OpenProjectPathOperation request, CancellationToken cancellationToken)
    {
        var projectResult = appData.Projects.GetById(request.FilePath);
        if (!projectResult.IsSuccess)
        {
            return Result.Fail($"No valid project found on {request.FilePath}");
        }

        return projectResult.Then(project =>
        {
            project.SetActive();
            appData.Projects.Update(project);
            appData.Save();

            return projectSession.StartSession(project);
        });
    }
}
