using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects.Operations;

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

        return await projectResult
            .ThenAsync(async project =>
            {
                project.SetActive();
                appData.Projects.Update(project);
                await appData.SaveAsync(cancellationToken).ConfigureAwait(false);

                return await projectSession
                    .StartSession(project, ct: cancellationToken)
                    .ConfigureAwait(false);
            })
            .ConfigureAwait(false);
    }
}
