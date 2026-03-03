using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.StorySettings.Operations;

public sealed record UpdateStorySettingDetailsOperation(StorySettingId SettingId, string Details) : IOperation;

internal sealed class UpdateStorySettingDetailsOperationHandler : IOperationHandler<UpdateStorySettingDetailsOperation>
{
    private readonly IDataSession data;

    public UpdateStorySettingDetailsOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateStorySettingDetailsOperation request, CancellationToken cancellationToken)
    {
        return await data.StorySettings.GetById(request.SettingId)
            .ThenAsync(async settings =>
            {
                settings.Details = request.Details;
                data.StorySettings.Update(settings);
                await data.SaveAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
