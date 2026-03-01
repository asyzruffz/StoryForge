using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters;

public sealed record UpdateCharacterImportanceOperation(CharacterId CharacterId, Importance Importance) : IOperation;

internal sealed class UpdateCharacterImportanceOperationHandler : IOperationHandler<UpdateCharacterImportanceOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterImportanceOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateCharacterImportanceOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return data.Characters.GetById(request.CharacterId)
            .Then(character =>
            {
                character.Importance = request.Importance;
                data.Characters.Update(character);
                data.Save();
                return Result.Ok();
            });
    }
}
