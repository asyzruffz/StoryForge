using Keystone;
using Keystone.Application;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Books.Operations;

public sealed record UpdateBookSeriesOperation(string Series) : IOperation;

internal sealed class UpdateBookSeriesOperationHandler : IOperationHandler<UpdateBookSeriesOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IDataSession data;

    public UpdateBookSeriesOperationHandler(IProjectSessionHandler projectSessionHandler, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateBookSeriesOperation request, CancellationToken cancellationToken)
    {
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return await data.Books.Get()
            .ThenAsync(async (book, ct) =>
            {
                book.Series = request.Series;
                data.Books.Update(book);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
