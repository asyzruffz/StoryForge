using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Plots.Operations;

public sealed record CreatePlotOperation(string Name) : IOperation;

internal sealed class CreatePlotOperationHandler : IOperationHandler<CreatePlotOperation>
{
    private readonly IDataSession data;

    public CreatePlotOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(CreatePlotOperation request, CancellationToken cancellationToken)
    {
        var newPlot = Plot.New(request.Name);
        data.Plots.Create(newPlot);
        await data.SaveAsync(cancellationToken).ConfigureAwait(false);
        return Result.Ok();
    }
}
