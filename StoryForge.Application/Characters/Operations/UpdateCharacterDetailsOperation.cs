using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterDetailsOperation(CharacterId CharacterId, string? Details) : IOperation;

internal sealed class UpdateCharacterDetailsOperationHandler : IOperationHandler<UpdateCharacterDetailsOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterDetailsOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateCharacterDetailsOperation request, CancellationToken cancellationToken)
    {
        return await data.Characters.GetById(request.CharacterId)
            .ToResult("Couldn't find character in database.")
            .ThenAsync(async (character, ct) =>
            {
                character.Details = request.Details;
                data.Characters.Update(character);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
