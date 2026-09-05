using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Plots.Operations;

public sealed record GetPlotOperation(PlotId Id) : IOperation<Plot>;

internal sealed class GetPlotOperationHandler : IOperationHandler<GetPlotOperation, Plot>
{
    private readonly IDataSession data;

    public GetPlotOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result<Plot>> Handle(GetPlotOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = data.Plots.GetById(request.Id)
            .ToResult("Couldn't find plot in database.");
        return result;
    }
}
