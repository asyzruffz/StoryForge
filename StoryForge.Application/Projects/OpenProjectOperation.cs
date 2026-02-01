using StoryForge.Application.Abstractions;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record OpenProjectOperation() : IOperation;

internal sealed class OpenProjectOperationHandler : IOperationHandler<OpenProjectOperation>
{
    public OpenProjectOperationHandler()
    {
        
    }

    public Task<Result> Handle(OpenProjectOperation request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Ok());
    }
}
