using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;
using System.Collections.Generic;

namespace StoryForge.Application.Characters;

public sealed record GetCharacterEntriesOperation : IOperation<IEnumerable<CharacterEntry>>;

internal sealed class GetCharacterEntriesOperationHandler : IOperationHandler<GetCharacterEntriesOperation, IEnumerable<CharacterEntry>>
{
    private readonly IDataSession data;

    public GetCharacterEntriesOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result<IEnumerable<CharacterEntry>>> Handle(GetCharacterEntriesOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var entries = data.Characters.GetAll()
            .Select(character => character.ToEntry());
        return Result<IEnumerable<CharacterEntry>>.Ok(entries);
    }
}
