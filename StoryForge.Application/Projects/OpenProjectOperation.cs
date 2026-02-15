using StoryForge.Application.Abstractions;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record OpenProjectOperation(string FilePath) : IOperation;

internal sealed class OpenProjectOperationHandler : IOperationHandler<OpenProjectOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IApplicationDataSession appData;

    public OpenProjectOperationHandler(IProjectSessionHandler projectSessionHandler, IApplicationDataSession appDataSession)
    {
        projectSession = projectSessionHandler;
        appData = appDataSession;
    }

    public async Task<Result> Handle(OpenProjectOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Result.Ok();
    }
}
