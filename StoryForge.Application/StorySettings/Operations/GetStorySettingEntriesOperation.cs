using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.StorySettings.Operations;

public sealed record GetStorySettingEntriesOperation : IOperation<IEnumerable<StorySettingEntry>>;

internal sealed class GetStorySettingEntriesOperationHandler : IOperationHandler<GetStorySettingEntriesOperation, IEnumerable<StorySettingEntry>>
{
    private readonly IDataSession data;

    public GetStorySettingEntriesOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result<IEnumerable<StorySettingEntry>>> Handle(GetStorySettingEntriesOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var entries = data.StorySettings.GetAll()
            .Select(setting => setting.ToEntry());
        return Result<IEnumerable<StorySettingEntry>>.Ok(entries);
    }
}
