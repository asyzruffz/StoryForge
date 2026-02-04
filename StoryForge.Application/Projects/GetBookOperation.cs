using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record GetBookOperation() : IOperation<Book>;

internal sealed class GetBookOperationHandler : IOperationHandler<GetBookOperation, Book>
{
    private readonly IDataSession data;

    public GetBookOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result<Book>> Handle(GetBookOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = data.Books.Get();
        return result;
    }
}
