using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Plots;

public sealed record UpdatePlotCharactersOperation(PlotId PlotId, string Characters) : IOperation;

internal sealed class UpdatePlotCharactersOperationHandler : IOperationHandler<UpdatePlotCharactersOperation>
{
    private readonly IDataSession data;

    public UpdatePlotCharactersOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdatePlotCharactersOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return data.Plots.GetById(request.PlotId)
            .Then(plot =>
            {
                plot.Characters = request.Characters;
                data.Plots.Update(plot);
                data.Save();
                return Result.Ok();
            });
    }
}
