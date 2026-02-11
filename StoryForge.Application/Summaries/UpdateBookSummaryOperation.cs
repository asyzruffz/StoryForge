using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Summaries;

public sealed record UpdateBookSummaryOperation(BookSummary Summary) : IOperation;

internal sealed class UpdateBookSummaryOperationHandler : IOperationHandler<UpdateBookSummaryOperation>
{
    private readonly IDataSession data;

    public UpdateBookSummaryOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateBookSummaryOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        data.Books.Get()
            .Then(book =>
            {
                book.Extra = request.Summary;
                data.Books.Update(book);
            });
        data.Save();
        return Result.Ok();
    }
}
