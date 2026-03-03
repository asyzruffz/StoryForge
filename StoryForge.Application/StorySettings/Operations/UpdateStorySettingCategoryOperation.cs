using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.StorySettings.Operations;

public sealed record UpdateStorySettingCategoryOperation(StorySettingId SettingId, StorySettingCategory Category) : IOperation;

internal sealed class UpdateStorySettingCategoryOperationHandler : IOperationHandler<UpdateStorySettingCategoryOperation>
{
    private readonly IDataSession data;

    public UpdateStorySettingCategoryOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateStorySettingCategoryOperation request, CancellationToken cancellationToken)
    {
        return await data.StorySettings.GetById(request.SettingId)
            .ThenAsync(async settings =>
            {
                settings.Category = request.Category;
                data.StorySettings.Update(settings);
                await data.SaveAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
