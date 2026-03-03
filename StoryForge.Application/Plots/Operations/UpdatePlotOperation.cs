using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Plots.Operations;

public sealed record UpdatePlotOperation(Plot Plot) : IOperation;

internal sealed class UpdatePlotOperationHandler : IOperationHandler<UpdatePlotOperation>
{
    private readonly IDataSession data;

    public UpdatePlotOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdatePlotOperation request, CancellationToken cancellationToken)
    {
        data.Plots.Update(request.Plot);
        await data.SaveAsync(cancellationToken).ConfigureAwait(false);
        return Result.Ok();
    }
}
