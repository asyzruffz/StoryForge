using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Characters;

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
        await Task.CompletedTask;
        data.Characters.Update(request.Character);
        data.Save();
        return Result.Ok();
    }
}
