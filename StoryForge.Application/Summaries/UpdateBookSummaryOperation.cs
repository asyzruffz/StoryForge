using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Summaries;

public sealed record UpdateBookSummaryOperation(BookSummary Summary) : IOperation;

internal sealed class UpdateBookSummaryOperationHandler : IOperationHandler<UpdateBookSummaryOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IDataSession data;

    public UpdateBookSummaryOperationHandler(IProjectSessionHandler projectSessionHandler, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateBookSummaryOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        var bookResult = data.Books.Get();
        if (!bookResult.IsSuccess)
        {
            return Result.Fail(bookResult.ErrorMessage);
        }

        bookResult.Then(book =>
        {
            book.Extra = request.Summary;
            data.Books.Update(book);
            data.Save();
        });

        return Result.Ok();
    }
}
