using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Chapters;

public sealed record AddChapterOperation(string Title) : IOperation;

internal sealed class AddChapterOperationHandler : IOperationHandler<AddChapterOperation>
{
    private readonly IDataSession data;

    public AddChapterOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(AddChapterOperation request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Title))
            return Result.Fail("Invalid title provided");

        var chapter = new Chapter
        {
            Id = ChapterId.New(),
            Title = new Title(request.Title),
        };

        data.Chapters.Create(chapter);

        await Task.CompletedTask;
        return Result.Ok();
    }
}
