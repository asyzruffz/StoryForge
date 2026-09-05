using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterImportanceOperation(CharacterId CharacterId, Importance Importance) : IOperation;

internal sealed class UpdateCharacterImportanceOperationHandler : IOperationHandler<UpdateCharacterImportanceOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterImportanceOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateCharacterImportanceOperation request, CancellationToken cancellationToken)
    {
        return await data.Characters.GetById(request.CharacterId)
            .ToResult("Couldn't find character in database.")
            .ThenAsync(async (character, ct) =>
            {
                character.Importance = request.Importance;
                data.Characters.Update(character);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
