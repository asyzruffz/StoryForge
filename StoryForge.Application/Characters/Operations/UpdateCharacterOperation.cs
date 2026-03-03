using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterOperation(Character Character) : IOperation;

internal sealed class UpdateCharacterOperationHandler : IOperationHandler<UpdateCharacterOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateCharacterOperation request, CancellationToken cancellationToken)
    {
        data.Characters.Update(request.Character);
        await data.SaveAsync(cancellationToken).ConfigureAwait(false);
        return Result.Ok();
    }
}
