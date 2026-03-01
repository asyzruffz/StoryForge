using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Plots;

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
        await Task.CompletedTask;
        return data.Plots.GetById(request.PlotId)
            .Then(plot =>
            {
                plot.Importance = request.Importance;
                data.Plots.Update(plot);
                data.Save();
                return Result.Ok();
            });
    }
}
