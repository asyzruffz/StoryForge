using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterConflictOperation(CharacterId CharacterId, string Conflict) : IOperation;

internal sealed class UpdateCharacterConflictOperationHandler : IOperationHandler<UpdateCharacterConflictOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterConflictOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateCharacterConflictOperation request, CancellationToken cancellationToken)
    {
        return await data.Characters.GetById(request.CharacterId)
            .ThenAsync(async (character, ct) =>
            {
                character.Conflict = request.Conflict;
                data.Characters.Update(character);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
