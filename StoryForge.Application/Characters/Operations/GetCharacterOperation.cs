using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Characters.Operations;

public sealed record GetCharacterOperation(CharacterId Id) : IOperation<Character>;

internal sealed class GetCharacterOperationHandler : IOperationHandler<GetCharacterOperation, Character>
{
    private readonly IDataSession data;

    public GetCharacterOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result<Character>> Handle(GetCharacterOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = data.Characters.GetById(request.Id)
            .ToResult("Couldn't find character in database.");
        return result;
    }
}
