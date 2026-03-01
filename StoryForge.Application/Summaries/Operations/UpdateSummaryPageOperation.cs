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
        await Task.CompletedTask;
        return data.Summaries.GetById(request.SummaryId)
            .Then(summary =>
            {
                summary.Page = request.Page;
                data.Summaries.Update(summary);
                data.Save();
                return Result.Ok();
            });
    }
}
