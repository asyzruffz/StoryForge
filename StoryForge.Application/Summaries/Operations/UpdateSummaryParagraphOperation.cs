using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Summaries.Operations;

public sealed record UpdateSummaryParagraphOperation(SummaryId SummaryId, string Paragraph) : IOperation;

internal sealed class UpdateSummaryParagraphOperationHandler : IOperationHandler<UpdateSummaryParagraphOperation>
{
    private readonly IDataSession data;

    public UpdateSummaryParagraphOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateSummaryParagraphOperation request, CancellationToken cancellationToken)
    {
        return await data.Summaries.GetById(request.SummaryId)
            .ThenAsync(async summary =>
            {
                summary.Paragraph = request.Paragraph;
                data.Summaries.Update(summary);
                await data.SaveAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
