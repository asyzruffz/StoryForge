using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Plots.Operations;

public sealed record UpdatePlotDescriptionOperation(PlotId PlotId, string Description) : IOperation;

internal sealed class UpdatePlotDescriptionOperationHandler : IOperationHandler<UpdatePlotDescriptionOperation>
{
    private readonly IDataSession data;

    public UpdatePlotDescriptionOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdatePlotDescriptionOperation request, CancellationToken cancellationToken)
    {
        return await data.Plots.GetById(request.PlotId)
            .ToResult("Couldn't find plot in database.")
            .ThenAsync(async (plot, ct) =>
            {
                plot.Description = request.Description;
                data.Plots.Update(plot);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
