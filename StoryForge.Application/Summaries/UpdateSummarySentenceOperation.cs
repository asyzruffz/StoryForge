using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Summaries;

public sealed record UpdateSummarySentenceOperation(SummaryId SummaryId, string Sentence) : IOperation;

internal sealed class UpdateSummarySentenceOperationHandler : IOperationHandler<UpdateSummarySentenceOperation>
{
    private readonly IDataSession data;

    public UpdateSummarySentenceOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateSummarySentenceOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return data.Summaries.GetById(request.SummaryId)
            .Then(summary =>
            {
                summary.Sentence = request.Sentence;
                data.Summaries.Update(summary);
                data.Save();
                return Result.Ok();
            });
    }
}
