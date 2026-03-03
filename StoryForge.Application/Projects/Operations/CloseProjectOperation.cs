using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects.Operations;

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
        await projectSession.StopSession().ConfigureAwait(false);
        return Result.Ok();
    }
}
