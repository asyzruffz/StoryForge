using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters;

public sealed record UpdateCharacterNotesOperation(CharacterId CharacterId, string? Notes) : IOperation;

internal sealed class UpdateCharacterNotesOperationHandler : IOperationHandler<UpdateCharacterNotesOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterNotesOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateCharacterNotesOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        return data.Characters.GetById(request.CharacterId)
            .Then(character =>
            {
                character.Notes = request.Notes;
                data.Characters.Update(character);
                data.Save();
                return Result.Ok();
            });
    }
}
