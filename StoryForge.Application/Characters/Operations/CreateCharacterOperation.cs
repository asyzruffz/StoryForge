using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters.Operations;

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
        var newCharacter = Character.New(request.Name);
        data.Characters.Create(newCharacter);
        await data.SaveAsync(cancellationToken).ConfigureAwait(false);
        return Result.Ok();
    }
}
