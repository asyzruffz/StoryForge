using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Characters.Operations;

public sealed record GetCharacterEntriesOperation : IOperation<IEnumerable<CharacterEntry>>;

internal sealed class GetCharacterEntriesOperationHandler : IOperationHandler<GetCharacterEntriesOperation, IEnumerable<CharacterEntry>>
{
    private readonly IDataSession data;

    public GetCharacterEntriesOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result<IEnumerable<CharacterEntry>>> Handle(GetCharacterEntriesOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var entries = data.Characters.GetAll()
            .Select(character => character.ToEntry());
        return Result<IEnumerable<CharacterEntry>>.Ok(entries);
    }
}
