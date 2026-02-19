using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Plots;

public sealed record GetPlotOperation(PlotId Id) : IOperation<Plot>;

internal sealed class GetPlotOperationHandler : IOperationHandler<GetPlotOperation, Plot>
{
    private readonly IDataSession data;

    public GetPlotOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result<Plot>> Handle(GetPlotOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = data.Plots.GetById(request.Id);
        return result;
    }
}
