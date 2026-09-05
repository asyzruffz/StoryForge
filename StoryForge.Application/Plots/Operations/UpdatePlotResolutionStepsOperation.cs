using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Plots.Operations;

public sealed record UpdatePlotResolutionStepsOperation(PlotId PlotId, string ResolutionSteps) : IOperation;

internal sealed class UpdatePlotResolutionStepsOperationHandler : IOperationHandler<UpdatePlotResolutionStepsOperation>
{
    private readonly IDataSession data;

    public UpdatePlotResolutionStepsOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdatePlotResolutionStepsOperation request, CancellationToken cancellationToken)
    {
        return await data.Plots.GetById(request.PlotId)
            .ToResult("Couldn't find plot in database.")
            .ThenAsync(async (plot, ct) =>
            {
                plot.ResolutionSteps = request.ResolutionSteps;
                data.Plots.Update(plot);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
