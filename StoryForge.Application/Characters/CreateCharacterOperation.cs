using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters;

public sealed record CreateCharacterOperation(string Name) : IOperation;

internal sealed class CreateCharacterOperationHandler : IOperationHandler<CreateCharacterOperation>
{

    private readonly IDataSession data;

    public CreateCharacterOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(CreateCharacterOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var newCharacter = Character.New(request.Name);
        data.Characters.Create(newCharacter);
        data.Save();
        return Result.Ok();
    }
}
