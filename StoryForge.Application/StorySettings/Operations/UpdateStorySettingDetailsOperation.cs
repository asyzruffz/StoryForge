using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.StorySettings.Operations;

public sealed record UpdateStorySettingDetailsOperation(StorySettingId SettingId, string Details) : IOperation;

internal sealed class UpdateStorySettingDetailsOperationHandler : IOperationHandler<UpdateStorySettingDetailsOperation>
{
    private readonly IDataSession data;

    public UpdateStorySettingDetailsOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateStorySettingDetailsOperation request, CancellationToken cancellationToken)
    {
        return await data.StorySettings.GetById(request.SettingId)
            .ToResult("Couldn't find setting in database.")
            .ThenAsync(async (settings, ct) =>
            {
                settings.Details = request.Details;
                data.StorySettings.Update(settings);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
