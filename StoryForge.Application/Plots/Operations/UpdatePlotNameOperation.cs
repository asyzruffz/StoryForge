using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Plots.Operations;

public sealed record UpdatePlotNameOperation(PlotId PlotId, string Name) : IOperation;

internal sealed class UpdatePlotNameOperationHandler : IOperationHandler<UpdatePlotNameOperation>
{
    private readonly IDataSession data;

    public UpdatePlotNameOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdatePlotNameOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return data.Plots.GetById(request.PlotId)
            .Then(plot =>
            {
                plot.Name = request.Name;
                data.Plots.Update(plot);
                data.Save();
                return Result.Ok();
            });
    }
}
