using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.StorySettings.Operations;

public sealed record UpdateStorySettingCategoryOperation(StorySettingId SettingId, StorySettingCategory Category) : IOperation;

internal sealed class UpdateStorySettingCategoryOperationHandler : IOperationHandler<UpdateStorySettingCategoryOperation>
{
    private readonly IDataSession data;

    public UpdateStorySettingCategoryOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateStorySettingCategoryOperation request, CancellationToken cancellationToken)
    {
        return await data.StorySettings.GetById(request.SettingId)
            .ToResult("Couldn't find setting in database.")
            .ThenAsync(async (settings, ct) =>
            {
                settings.Category = request.Category;
                data.StorySettings.Update(settings);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
