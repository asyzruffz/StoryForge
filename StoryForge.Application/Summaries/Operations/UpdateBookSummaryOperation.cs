using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Summaries.Operations;

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
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return await data.Books.Get()
            .ThenAsync(async book =>
            {
                book.Extra = request.Summary;
                data.Books.Update(book);
                await data.SaveAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
