using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Chapters;

public sealed record GetChaptersOperation : IOperation<IEnumerable<Chapter>>;

internal sealed class GetChaptersOperationHandler
    : IOperationHandler<GetChaptersOperation, IEnumerable<Chapter>>
{
    private readonly IDataSession data;

    public GetChaptersOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result<IEnumerable<Chapter>>> Handle(GetChaptersOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = data.Chapters.GetAll();
        return Result<IEnumerable<Chapter>>.Ok(result);
    }
}
