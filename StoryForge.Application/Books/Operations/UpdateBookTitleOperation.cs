using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Books.Operations;

public sealed record UpdateBookTitleOperation(string Title) : IOperation;

internal sealed class UpdateBookTitleOperationHandler : IOperationHandler<UpdateBookTitleOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IDataSession data;

    public UpdateBookTitleOperationHandler(IProjectSessionHandler projectSessionHandler, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateBookTitleOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return data.Books.Get()
            .Then(book =>
            {
                book.Title = request.Title;
                data.Books.Update(book);
                data.Save();
                return Result.Ok();
            });
    }
}
