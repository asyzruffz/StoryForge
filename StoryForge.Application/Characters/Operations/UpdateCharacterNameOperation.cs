using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterNameOperation(CharacterId CharacterId, string Name) : IOperation;

internal sealed class UpdateCharacterNameOperationHandler : IOperationHandler<UpdateCharacterNameOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterNameOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateCharacterNameOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return data.Characters.GetById(request.CharacterId)
            .Then(character =>
            {
                character.Name = request.Name;
                data.Characters.Update(character);
                data.Save();
                return Result.Ok();
            });
    }
}
