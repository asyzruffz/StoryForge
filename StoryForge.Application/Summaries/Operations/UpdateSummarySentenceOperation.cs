using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Summaries.Operations;

public sealed record UpdateSummarySentenceOperation(SummaryId SummaryId, string Sentence) : IOperation;

internal sealed class UpdateSummarySentenceOperationHandler : IOperationHandler<UpdateSummarySentenceOperation>
{
    private readonly IDataSession data;

    public UpdateSummarySentenceOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateSummarySentenceOperation request, CancellationToken cancellationToken)
    {
        return await data.Summaries.GetById(request.SummaryId)
            .ToResult("Couldn't find summary in database.")
            .ThenAsync(async (summary, ct) =>
            {
                summary.Sentence = request.Sentence;
                data.Summaries.Update(summary);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
