using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Plots;

public sealed record UpdatePlotOperation(Plot Plot) : IOperation;

internal sealed class UpdatePlotOperationHandler : IOperationHandler<UpdatePlotOperation>
{
    private readonly IDataSession data;

    public UpdatePlotOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdatePlotOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        data.Plots.Update(request.Plot);
        data.Save();
        return Result.Ok();
    }
}
