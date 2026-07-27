using Keystone;
using Keystone.Application;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;

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

    public async ValueTask<Result> Handle(UpdateBookTitleOperation request, CancellationToken cancellationToken)
    {
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return await data.Books.Get()
            .ThenAsync(async (book, ct) =>
            {
                book.Title = request.Title;
                data.Books.Update(book);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
