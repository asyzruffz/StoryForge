using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record UpdateAuthorOperation(Author Author) : IOperation;

internal sealed class UpdateAuthorOperationHandler : IOperationHandler<UpdateAuthorOperation>
{
    private readonly IDataSession data;

    public UpdateAuthorOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateAuthorOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        data.Authors.Update(request.Author);
        return Result.Ok();
    }
}
