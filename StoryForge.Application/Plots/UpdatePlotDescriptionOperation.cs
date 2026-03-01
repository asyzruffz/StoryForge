using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Plots;

public sealed record UpdatePlotDescriptionOperation(PlotId PlotId, string Description) : IOperation;

internal sealed class UpdatePlotDescriptionOperationHandler : IOperationHandler<UpdatePlotDescriptionOperation>
{
    private readonly IDataSession data;

    public UpdatePlotDescriptionOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdatePlotDescriptionOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return data.Plots.GetById(request.PlotId)
            .Then(plot =>
            {
                plot.Description = request.Description;
                data.Plots.Update(plot);
                data.Save();
                return Result.Ok();
            });
    }
}
