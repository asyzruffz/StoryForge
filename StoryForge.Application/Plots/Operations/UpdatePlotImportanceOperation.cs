using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Plots.Operations;

public sealed record UpdatePlotImportanceOperation(PlotId PlotId, Importance Importance) : IOperation;

internal sealed class UpdatePlotImportanceOperationHandler : IOperationHandler<UpdatePlotImportanceOperation>
{
    private readonly IDataSession data;

    public UpdatePlotImportanceOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdatePlotImportanceOperation request, CancellationToken cancellationToken)
    {
        return await data.Plots.GetById(request.PlotId)
            .ThenAsync(async plot =>
            {
                plot.Importance = request.Importance;
                data.Plots.Update(plot);
                await data.SaveAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
