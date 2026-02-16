using StoryForge.Application.Abstractions;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public record GetProjectSessionStatus : IOperation<bool>;

internal sealed class GetProjectSessionStatusHandler : IOperationHandler<GetProjectSessionStatus, bool>
{
    private readonly IProjectSessionHandler projectSession;

    public GetProjectSessionStatusHandler(IProjectSessionHandler projectSessionHandler)
    {
        projectSession = projectSessionHandler;
    }

    public Task<Result<bool>> Handle(GetProjectSessionStatus request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result<bool>.Ok(projectSession.IsActive));
    }
}
