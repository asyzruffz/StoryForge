using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record UpdateBookOperation(Book Book) : IOperation;

internal sealed class UpdateBookOperationHandler : IOperationHandler<UpdateBookOperation>
{
    private readonly IDataSession data;

    public UpdateBookOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateBookOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        data.Books.Update(request.Book);
        return Result.Ok();
    }
}
