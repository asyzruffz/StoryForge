using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Plots;

public sealed record CreatePlotOperation(string Name) : IOperation;

internal sealed class CreatePlotOperationHandler : IOperationHandler<CreatePlotOperation>
{
    private readonly IDataSession data;

    public CreatePlotOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(CreatePlotOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var newPlot = Plot.New(request.Name);
        data.Plots.Create(newPlot);
        data.Save();
        return Result.Ok();
    }
}
