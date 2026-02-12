using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.StorySettings;

public sealed record CreateStorySettingOperation(string Name) : IOperation;

internal sealed class CreateStorySettingOperationHandler : IOperationHandler<CreateStorySettingOperation>
{
    private readonly IDataSession data;

    public CreateStorySettingOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(CreateStorySettingOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var newSetting = StorySetting.New(request.Name);
        data.StorySettings.Create(newSetting);
        data.Save();
        return Result.Ok();
    }
}
