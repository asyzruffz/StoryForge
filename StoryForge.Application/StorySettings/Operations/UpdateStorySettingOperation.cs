using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.StorySettings.Operations;

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
        data.StorySettings.Update(request.Setting);
        await data.SaveAsync(cancellationToken).ConfigureAwait(false);
        return Result.Ok();
    }
}
