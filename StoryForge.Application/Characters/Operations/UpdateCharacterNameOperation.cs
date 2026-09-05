using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterNameOperation(CharacterId CharacterId, string Name) : IOperation;

internal sealed class UpdateCharacterNameOperationHandler : IOperationHandler<UpdateCharacterNameOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterNameOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateCharacterNameOperation request, CancellationToken cancellationToken)
    {
        return await data.Characters.GetById(request.CharacterId)
            .ToResult("Couldn't find character in database.")
            .ThenAsync(async (character, ct) =>
            {
                character.Name = request.Name;
                data.Characters.Update(character);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
