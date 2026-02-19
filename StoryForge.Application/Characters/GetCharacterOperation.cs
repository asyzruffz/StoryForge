using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters;

public sealed record GetCharacterOperation(CharacterId Id) : IOperation<Character>;

internal sealed class GetCharacterOperationHandler : IOperationHandler<GetCharacterOperation, Character>
{
    private readonly IDataSession data;

    public GetCharacterOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result<Character>> Handle(GetCharacterOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = data.Characters.GetById(request.Id);
        return result;
    }
}
