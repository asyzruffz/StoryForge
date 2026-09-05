using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.StorySettings.Operations;

public sealed record UpdateStorySettingOperation(StorySetting Setting) : IOperation;

internal sealed class UpdateStorySettingHandler : IOperationHandler<UpdateStorySettingOperation>
{
    private readonly IDataSession data;

    public UpdateStorySettingHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateStorySettingOperation request, CancellationToken cancellationToken)
    {
        data.StorySettings.Update(request.Setting);
        await data.SaveAsync(cancellationToken).ConfigureAwait(false);
        return Result.Ok();
    }
}
