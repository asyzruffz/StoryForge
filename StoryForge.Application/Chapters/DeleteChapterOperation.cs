using Keystone;
using Keystone.Application;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Chapters;

public sealed record DeleteChapterOperation(string ShortId) : IOperation;

internal sealed class DeleteChapterOperationHandler : IOperationHandler<DeleteChapterOperation>
{
    private readonly IDataSession data;

    public DeleteChapterOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async ValueTask<Result> Handle(DeleteChapterOperation request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.ShortId))
        {
            return Result.Fail("Invalid id provided");
        }

        var result = data.Chapters.GetAll()
            .SingleOrDefault(chapter => chapter.Id.Value.Contains(request.ShortId));

        if (result == null)
        {
            return Result.Fail($"Chapter with id [{request.ShortId}] not found");
        }

        data.Chapters.Delete(result);
        await Task.CompletedTask;
        return Result.Ok();
    }
}
