using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters;

public sealed record UpdateCharacterGoalOperation(CharacterId CharacterId, string Goal) : IOperation;

internal sealed class UpdateCharacterGoalOperationHandler : IOperationHandler<UpdateCharacterGoalOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterGoalOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateCharacterGoalOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return data.Characters.GetById(request.CharacterId)
            .Then(character =>
            {
                character.Goal = request.Goal;
                data.Characters.Update(character);
                data.Save();
                return Result.Ok();
            });
    }
}
