using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters;

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
        await Task.CompletedTask;
        return data.Characters.GetById(request.CharacterId)
            .Then(character =>
            {
                character.Conflict = request.Conflict;
                data.Characters.Update(character);
                data.Save();
                return Result.Ok();
            });
    }
}
