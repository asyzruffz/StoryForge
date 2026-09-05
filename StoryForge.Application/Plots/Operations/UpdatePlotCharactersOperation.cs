using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Plots.Operations;

public sealed record UpdatePlotCharactersOperation(PlotId PlotId, string Characters) : IOperation;

internal sealed class UpdatePlotCharactersOperationHandler : IOperationHandler<UpdatePlotCharactersOperation>
{
    private readonly IDataSession data;

    public UpdatePlotCharactersOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdatePlotCharactersOperation request, CancellationToken cancellationToken)
    {
        return await data.Plots.GetById(request.PlotId)
            .ToResult("Couldn't find plot in database.")
            .ThenAsync(async (plot, ct) =>
            {
                plot.Characters = request.Characters;
                data.Plots.Update(plot);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
