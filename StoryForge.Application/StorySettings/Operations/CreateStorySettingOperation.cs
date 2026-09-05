using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.StorySettings.Operations;

public sealed record CreateStorySettingOperation(string Name) : IOperation;

internal sealed class CreateStorySettingOperationHandler : IOperationHandler<CreateStorySettingOperation>
{
    private readonly IDataSession data;

    public CreateStorySettingOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(CreateStorySettingOperation request, CancellationToken cancellationToken)
    {
        var newSetting = StorySetting.New(request.Name);
        data.StorySettings.Create(newSetting);
        await data.SaveAsync(cancellationToken).ConfigureAwait(false);
        return Result.Ok();
    }
}
