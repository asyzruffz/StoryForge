using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record GetAuthorOperation : IOperation<Author>;

internal sealed class GetAuthorOperationHandler : IOperationHandler<GetAuthorOperation, Author>
{
    private readonly IDataSession data;

    public GetAuthorOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result<Author>> Handle(GetAuthorOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = data.Authors.Get();
        return result;
    }
}
