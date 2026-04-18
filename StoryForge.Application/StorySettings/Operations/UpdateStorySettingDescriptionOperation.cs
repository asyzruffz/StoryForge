using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.StorySettings.Operations;

public sealed record UpdateStorySettingDescriptionOperation(StorySettingId SettingId, string Description) : IOperation;

internal sealed class UpdateStorySettingDescriptionOperationHandler : IOperationHandler<UpdateStorySettingDescriptionOperation>
{
    private readonly IDataSession data;

    public UpdateStorySettingDescriptionOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateStorySettingDescriptionOperation request, CancellationToken cancellationToken)
    {
        return await data.StorySettings.GetById(request.SettingId)
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
