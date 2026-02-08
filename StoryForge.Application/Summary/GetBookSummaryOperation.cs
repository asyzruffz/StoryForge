using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Summary;

public sealed record GetBookSummaryOperation : IOperation<BookSummary>;

internal sealed class GetBookSummaryOperationHandler : IOperationHandler<GetBookSummaryOperation, BookSummary>
{
    private readonly IDataSession data;

    public GetBookSummaryOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result<BookSummary>> Handle(GetBookSummaryOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = data.Books.Get()
            .Then(book => Result<BookSummary>.Ok(book.Extra));
        return result;
    }
}
