using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Summaries.Operations;

public sealed record UpdateSummaryOperation(Summary Summary) : IOperation;

internal sealed class UpdateSummaryOperationHandler : IOperationHandler<UpdateSummaryOperation>
{
    private readonly IDataSession data;

    public UpdateSummaryOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateSummaryOperation request, CancellationToken cancellationToken)
    {
        data.Summaries.Update(request.Summary);
        await data.SaveAsync(cancellationToken).ConfigureAwait(false);
        return Result.Ok();
    }
}
