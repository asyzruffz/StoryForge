using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterNotesOperation(CharacterId CharacterId, string? Notes) : IOperation;

internal sealed class UpdateCharacterNotesOperationHandler : IOperationHandler<UpdateCharacterNotesOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterNotesOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateCharacterNotesOperation request, CancellationToken cancellationToken)
    {
        return await data.Characters.GetById(request.CharacterId)
            .ThenAsync(async character =>
            {
                character.Notes = request.Notes;
                data.Characters.Update(character);
                await data.SaveAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
