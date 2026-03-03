using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterMotivationOperation(CharacterId CharacterId, string Motivation) : IOperation;

internal sealed class UpdateCharacterMotivationOperationHandler : IOperationHandler<UpdateCharacterMotivationOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterMotivationOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateCharacterMotivationOperation request, CancellationToken cancellationToken)
    {
        return await data.Characters.GetById(request.CharacterId)
            .ThenAsync(async character =>
            {
                character.Motivation = request.Motivation;
                data.Characters.Update(character);
                await data.SaveAsync(cancellationToken).ConfigureAwait(false);
                return Result.Ok();
            })
            .ConfigureAwait(false);
    }
}
