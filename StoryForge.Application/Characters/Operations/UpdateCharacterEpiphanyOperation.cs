using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterEpiphanyOperation(CharacterId CharacterId, string Epiphany) : IOperation;

internal sealed class UpdateCharacterEpiphanyOperationHandler : IOperationHandler<UpdateCharacterEpiphanyOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterEpiphanyOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateCharacterEpiphanyOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return data.Characters.GetById(request.CharacterId)
            .Then(character =>
            {
                character.Epiphany = request.Epiphany;
                data.Characters.Update(character);
                data.Save();
                return Result.Ok();
            });
    }
}
