using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Characters.Operations;

public sealed record UpdateCharacterOperation(Character Character) : IOperation;

internal sealed class UpdateCharacterOperationHandler : IOperationHandler<UpdateCharacterOperation>
{
    private readonly IDataSession data;

    public UpdateCharacterOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateCharacterOperation request, CancellationToken cancellationToken)
    {
        data.Characters.Update(request.Character);
        await data.SaveAsync(cancellationToken).ConfigureAwait(false);
        return Result.Ok();
    }
}
