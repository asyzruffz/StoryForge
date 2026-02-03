using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record GetBookOperation() : IOperation<Book>;

internal sealed class GetBookOperationHandler : IOperationHandler<GetBookOperation, Book>
{
    public GetBookOperationHandler()
    {

    }

    public async Task<Result<Book>> Handle(GetBookOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return Result<Book>.Ok(new Book());
    }
}
