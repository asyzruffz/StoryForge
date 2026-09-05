using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.StorySettings.Operations;

public sealed record GetStorySettingOperation(StorySettingId Id) : IOperation<StorySetting>;

internal sealed class GetStorySettingOperationHandler : IOperationHandler<GetStorySettingOperation, StorySetting>
{
    private readonly IDataSession data;

    public GetStorySettingOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result<StorySetting>> Handle(GetStorySettingOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = data.StorySettings.GetById(request.Id)
            .ToResult("Couldn't find setting in database.");
        return result;
    }
}
