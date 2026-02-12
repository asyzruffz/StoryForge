using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.StorySettings;

public sealed record UpdateStorySettingOperation(StorySetting Setting) : IOperation;

internal sealed class UpdateStorySettingHandler : IOperationHandler<UpdateStorySettingOperation>
{
    private readonly IDataSession data;

    public UpdateStorySettingHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateStorySettingOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        data.StorySettings.Update(request.Setting);
        data.Save();
        return Result.Ok();
    }
}
