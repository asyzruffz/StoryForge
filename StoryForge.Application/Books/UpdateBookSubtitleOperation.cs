using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Books;

public sealed record UpdateBookSubtitleOperation(string Subtitle) : IOperation;

internal sealed class UpdateBookSubtitleOperationHandler : IOperationHandler<UpdateBookSubtitleOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IDataSession data;

    public UpdateBookSubtitleOperationHandler(IProjectSessionHandler projectSessionHandler, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateBookSubtitleOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return data.Books.Get()
            .Then(book =>
            {
                book.Subtitle = request.Subtitle;
                data.Books.Update(book);
                data.Save();
                return Result.Ok();
            });
    }
}
