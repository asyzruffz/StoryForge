using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
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
        var result = data.Chapters.GetAll();
        await Task.CompletedTask;
        return Result<IEnumerable<Chapter>>.Ok(result);
    }
}
