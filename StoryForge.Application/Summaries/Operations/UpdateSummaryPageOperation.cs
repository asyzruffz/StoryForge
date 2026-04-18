using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Summaries.Operations;

public sealed record UpdateSummaryPageOperation(SummaryId SummaryId, string Page) : IOperation;

internal sealed class UpdateSummaryPageOperationHandler : IOperationHandler<UpdateSummaryPageOperation>
{
    private readonly IDataSession data;

    public UpdateSummaryPageOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateSummaryPageOperation request, CancellationToken cancellationToken)
    {
        return await data.Summaries.GetById(request.SummaryId)
            .ThenAsync(async (summary, ct) =>
            {
                summary.Page = request.Page;
                data.Summaries.Update(summary);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
