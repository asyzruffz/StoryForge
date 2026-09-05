using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterGoalOperation(CharacterId CharacterId, string Goal) : IOperation;

internal sealed class UpdateCharacterGoalOperationHandler : IOperationHandler<UpdateCharacterGoalOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterGoalOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateCharacterGoalOperation request, CancellationToken cancellationToken)
    {
        return await data.Characters.GetById(request.CharacterId)
            .ToResult("Couldn't find character in database.")
            .ThenAsync(async (character, ct) =>
            {
                character.Goal = request.Goal;
                data.Characters.Update(character);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
