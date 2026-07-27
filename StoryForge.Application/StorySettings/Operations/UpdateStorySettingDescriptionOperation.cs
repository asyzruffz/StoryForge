using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.StorySettings.Operations;

public sealed record UpdateStorySettingDescriptionOperation(StorySettingId SettingId, string Description) : IOperation;

internal sealed class UpdateStorySettingDescriptionOperationHandler : IOperationHandler<UpdateStorySettingDescriptionOperation>
{
    private readonly IDataSession data;

    public UpdateStorySettingDescriptionOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateStorySettingDescriptionOperation request, CancellationToken cancellationToken)
    {
        return await data.StorySettings.GetById(request.SettingId)
            .ToResult("Couldn't find setting in database.")
            .ThenAsync(async (settings, ct) =>
            {
                settings.Description = request.Description;
                data.StorySettings.Update(settings);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
