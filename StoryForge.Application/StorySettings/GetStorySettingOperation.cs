using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.StorySettings;

public sealed record GetStorySettingOperation(StorySettingId Id) : IOperation<StorySetting>;

internal sealed class GetStorySettingOperationHandler : IOperationHandler<GetStorySettingOperation, StorySetting>
{
    private readonly IDataSession data;

    public GetStorySettingOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result<StorySetting>> Handle(GetStorySettingOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = data.StorySettings.GetById(request.Id);
        return result;
    }
}
