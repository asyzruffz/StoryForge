using StoryForge.Application.Abstractions;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record CloseProjectOperation : IOperation;

internal sealed class CloseProjectOperationHandler : IOperationHandler<CloseProjectOperation>
{
    private readonly IProjectSessionHandler projectSession;

    public CloseProjectOperationHandler(IProjectSessionHandler projectSessionHandler)
    {
        projectSession = projectSessionHandler;
    }

    public async Task<Result> Handle(CloseProjectOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        projectSession.StopSession();
        return Result.Ok();
    }
}
