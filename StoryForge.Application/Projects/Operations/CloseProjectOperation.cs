using Keystone;
using Keystone.Application;
using StoryForge.Core.Projects;

namespace StoryForge.Application.Projects.Operations;

public sealed record CloseProjectOperation : IOperation;

internal sealed class CloseProjectOperationHandler : IOperationHandler<CloseProjectOperation>
{
    private readonly IProjectSessionHandler projectSession;

    public CloseProjectOperationHandler(IProjectSessionHandler projectSessionHandler)
    {
        projectSession = projectSessionHandler;
    }

    public async ValueTask<Result> Handle(CloseProjectOperation request, CancellationToken cancellationToken)
    {
        await projectSession.StopSession().ConfigureAwait(false);
        return Result.Ok();
    }
}
