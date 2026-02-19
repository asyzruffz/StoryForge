using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Summaries;

public sealed record GetBookSummaryOperation : IOperation<BookSummary>;

internal sealed class GetBookSummaryOperationHandler : IOperationHandler<GetBookSummaryOperation, BookSummary>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IDataSession data;

    public GetBookSummaryOperationHandler(IProjectSessionHandler projectSessionHandler, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async Task<Result<BookSummary>> Handle(GetBookSummaryOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result<BookSummary>.Fail("No project is open");
        }

        return data.Books.Get()
            .Then(book => Result<BookSummary>.Ok(book.Extra));
    }
}
