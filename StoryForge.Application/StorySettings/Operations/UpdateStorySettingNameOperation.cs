using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.StorySettings.Operations;

public sealed record UpdateStorySettingNameOperation(StorySettingId SettingId, string Name) : IOperation;

internal sealed class UpdateStorySettingNameOperationHandler : IOperationHandler<UpdateStorySettingNameOperation>
{
    private readonly IDataSession data;

    public UpdateStorySettingNameOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateStorySettingNameOperation request, CancellationToken cancellationToken)
    {
        return await data.StorySettings.GetById(request.SettingId)
            .ThenAsync(async settings =>
            {
                settings.Name = request.Name;
                data.StorySettings.Update(settings);
                await data.SaveAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
