using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Plots.Operations;

public sealed record UpdatePlotResultOperation(PlotId PlotId, string Result) : IOperation;

internal sealed class UpdatePlotResultOperationHandler : IOperationHandler<UpdatePlotResultOperation>
{
    private readonly IDataSession data;

    public UpdatePlotResultOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdatePlotResultOperation request, CancellationToken cancellationToken)
    {
        return await data.Plots.GetById(request.PlotId)
            .ThenAsync(async (plot, ct) =>
            {
                plot.Result = request.Result;
                data.Plots.Update(plot);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
