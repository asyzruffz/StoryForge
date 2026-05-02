using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects.Operations;

public sealed record OpenProjectPathOperation(string FilePath) : IOperation;

internal sealed class OpenProjectPathOperationHandler : IOperationHandler<OpenProjectPathOperation>
{
    private readonly IProjectSessionHandler projectSession;

    public OpenProjectPathOperationHandler(IProjectSessionHandler projectSessionHandler)
    {
        projectSession = projectSessionHandler;
    }

    public Task<Result> Handle(OpenProjectPathOperation request, CancellationToken cancellationToken)
    {
        return projectSession.LoadSession(request.FilePath, cancellationToken);
    }
}
