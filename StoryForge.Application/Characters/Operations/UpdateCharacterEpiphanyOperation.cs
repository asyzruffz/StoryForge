using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterEpiphanyOperation(CharacterId CharacterId, string Epiphany) : IOperation;

internal sealed class UpdateCharacterEpiphanyOperationHandler : IOperationHandler<UpdateCharacterEpiphanyOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterEpiphanyOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateCharacterEpiphanyOperation request, CancellationToken cancellationToken)
    {
        return await data.Characters.GetById(request.CharacterId)
            .ToResult("Couldn't find character in database.")
            .ThenAsync(async (character, ct) =>
            {
                character.Epiphany = request.Epiphany;
                data.Characters.Update(character);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
