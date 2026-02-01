using StoryForge.Application.Abstractions;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record CreateProjectOperation(string Name) : IOperation;

internal sealed class CreateProjectOperationHandler : IOperationHandler<CreateProjectOperation>
{
    public CreateProjectOperationHandler()
    {
        
    }

    public Task<Result> Handle(CreateProjectOperation request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Result.Ok());
    }
}
