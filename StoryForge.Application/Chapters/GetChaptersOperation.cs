using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Storage;

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

    public async ValueTask<Result<IEnumerable<Chapter>>> Handle(GetChaptersOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var result = data.Chapters.GetAll();
        return Result<IEnumerable<Chapter>>.Ok(result);
    }
}
