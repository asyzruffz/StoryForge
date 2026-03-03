using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterDetailsOperation(CharacterId CharacterId, string? Details) : IOperation;

internal sealed class UpdateCharacterDetailsOperationHandler : IOperationHandler<UpdateCharacterDetailsOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterDetailsOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateCharacterDetailsOperation request, CancellationToken cancellationToken)
    {
        return await data.Characters.GetById(request.CharacterId)
            .ThenAsync(async character =>
            {
                character.Details = request.Details;
                data.Characters.Update(character);
                await data.SaveAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
