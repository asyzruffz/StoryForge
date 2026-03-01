using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters;

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
        await Task.CompletedTask;
        return data.Characters.GetById(request.CharacterId)
            .Then(character =>
            {
                character.Details = request.Details;
                data.Characters.Update(character);
                data.Save();
                return Result.Ok();
            });
    }
}
