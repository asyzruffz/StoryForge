using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Plots;

public sealed record GetPlotEntriesOperation : IOperation<IEnumerable<PlotEntry>>;

internal sealed class GetPlotEntriesOperationHandler : IOperationHandler<GetPlotEntriesOperation, IEnumerable<PlotEntry>>
{
    private readonly IDataSession data;

    public GetPlotEntriesOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result<IEnumerable<PlotEntry>>> Handle(GetPlotEntriesOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var entries = data.Plots.GetAll()
            .Select(plot => plot.ToEntry());
        return Result<IEnumerable<PlotEntry>>.Ok(entries);
    }
}
