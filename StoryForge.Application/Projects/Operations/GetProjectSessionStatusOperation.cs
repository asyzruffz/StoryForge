using Keystone;
using Keystone.Application;
using StoryForge.Core.Projects;

namespace StoryForge.Application.Projects.Operations;

public record GetProjectSessionStatusOperation : IOperation<bool>;

internal sealed class GetProjectSessionStatusHandler : IOperationHandler<GetProjectSessionStatusOperation, bool>
{
    private readonly IProjectSessionHandler projectSession;

    public GetProjectSessionStatusHandler(IProjectSessionHandler projectSessionHandler)
    {
        projectSession = projectSessionHandler;
    }

    public ValueTask<Result<bool>> Handle(GetProjectSessionStatusOperation request, CancellationToken cancellationToken)
    {
        return ValueTask.FromResult(Result<bool>.Ok(projectSession.IsActive));
    }
}
