using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Plots.Operations;

public sealed record UpdatePlotImportanceOperation(PlotId PlotId, Importance Importance) : IOperation;

internal sealed class UpdatePlotImportanceOperationHandler : IOperationHandler<UpdatePlotImportanceOperation>
{
    private readonly IDataSession data;

    public UpdatePlotImportanceOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdatePlotImportanceOperation request, CancellationToken cancellationToken)
    {
        return await data.Plots.GetById(request.PlotId)
            .ToResult("Couldn't find plot in database.")
            .ThenAsync(async (plot, ct) =>
            {
                plot.Importance = request.Importance;
                data.Plots.Update(plot);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
