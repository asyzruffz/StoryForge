using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects.Operations;

public record GetProjectSessionStatusOperation : IOperation<bool>;

internal sealed class GetProjectSessionStatusHandler : IOperationHandler<GetProjectSessionStatusOperation, bool>
{
    private readonly IProjectSessionHandler projectSession;

    public GetProjectSessionStatusHandler(IProjectSessionHandler projectSessionHandler)
    {
        projectSession = projectSessionHandler;
    }

    public Task<Result<bool>> Handle(GetProjectSessionStatusOperation request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result<bool>.Ok(projectSession.IsActive));
    }
}
