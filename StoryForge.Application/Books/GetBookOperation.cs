using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Books;

public sealed record GetBookOperation() : IOperation<Book>;

internal sealed class GetBookOperationHandler : IOperationHandler<GetBookOperation, Book>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IDataSession data;

    public GetBookOperationHandler(IProjectSessionHandler projectSessionHandler, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async Task<Result<Book>> Handle(GetBookOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result<Book>.Fail("No project is open");
        }

        return data.Books.Get();
    }
}
