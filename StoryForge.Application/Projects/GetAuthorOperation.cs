using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record GetAuthorOperation : IOperation<Author>;

internal sealed class GetAuthorOperationHandler : IOperationHandler<GetAuthorOperation, Author>
{
    public GetAuthorOperationHandler()
    {
        
    }

    public async Task<Result<Author>> Handle(GetAuthorOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Result<Author>.Ok(new Author());
    }
}