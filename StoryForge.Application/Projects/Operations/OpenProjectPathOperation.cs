using Keystone;
using Keystone.Application;
using StoryForge.Core.Projects;

namespace StoryForge.Application.Projects.Operations;

public sealed record OpenProjectPathOperation(string FilePath) : IOperation;

internal sealed class OpenProjectPathOperationHandler : IOperationHandler<OpenProjectPathOperation>
{
    private readonly IProjectSessionHandler projectSession;

    public OpenProjectPathOperationHandler(IProjectSessionHandler projectSessionHandler)
    {
        projectSession = projectSessionHandler;
    }

    public ValueTask<Result> Handle(OpenProjectPathOperation request, CancellationToken cancellationToken)
    {
        return projectSession.LoadSession(request.FilePath, cancellationToken);
    }
}
