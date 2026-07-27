using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Summaries.Operations;

public sealed record UpdateSummaryParagraphOperation(SummaryId SummaryId, string Paragraph) : IOperation;

internal sealed class UpdateSummaryParagraphOperationHandler : IOperationHandler<UpdateSummaryParagraphOperation>
{
    private readonly IDataSession data;

    public UpdateSummaryParagraphOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateSummaryParagraphOperation request, CancellationToken cancellationToken)
    {
        return await data.Summaries.GetById(request.SummaryId)
            .ToResult("Couldn't find summary in database.")
            .ThenAsync(async (summary, ct) =>
            {
                summary.Paragraph = request.Paragraph;
                data.Summaries.Update(summary);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
