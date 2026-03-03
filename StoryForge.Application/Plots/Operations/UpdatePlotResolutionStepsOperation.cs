using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Plots.Operations;

public sealed record UpdatePlotResolutionStepsOperation(PlotId PlotId, string ResolutionSteps) : IOperation;

internal sealed class UpdatePlotResolutionStepsOperationHandler : IOperationHandler<UpdatePlotResolutionStepsOperation>
{
    private readonly IDataSession data;

    public UpdatePlotResolutionStepsOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdatePlotResolutionStepsOperation request, CancellationToken cancellationToken)
    {
        return await data.Plots.GetById(request.PlotId)
            .ThenAsync(async plot =>
            {
                plot.ResolutionSteps = request.ResolutionSteps;
                data.Plots.Update(plot);
                await data.SaveAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
