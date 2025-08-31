using StoryForge.Application.Abstractions;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Chapters;

public sealed record LinkChapterOperation(string PreviousChapterShortId, string NextChapterShortId) : IOperation;

internal sealed class LinkChapterOperationHandler : IOperationHandler<LinkChapterOperation>
{
    private readonly IDataSession data;

    public LinkChapterOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(LinkChapterOperation request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.PreviousChapterShortId))
        {
            return Result.Fail("Invalid id for previous chapter provided");
        }
        if (string.IsNullOrWhiteSpace(request.NextChapterShortId))
        {
            return Result.Fail("Invalid id for next chapter provided");
        }

        var prevChapter = data.Chapters.GetAll()
            .SingleOrDefault(chapter => chapter.Id.Value.Contains(request.PreviousChapterShortId));
        if (prevChapter == null)
        {
            return Result.Fail($"Chapter with id [{request.PreviousChapterShortId}] not found");
        }

        var nextChapter = data.Chapters.GetAll()
            .SingleOrDefault(chapter => chapter.Id.Value.Contains(request.NextChapterShortId));
        if (nextChapter == null)
        {
            return Result.Fail($"Chapter with id [{request.NextChapterShortId}] not found");
        }

        prevChapter.SetNext(nextChapter);
        await Task.CompletedTask;
        return Result.Ok();
    }
}
