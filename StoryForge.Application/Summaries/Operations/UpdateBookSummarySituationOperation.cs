using Keystone;
using Keystone.Application;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Summaries.Operations;

public sealed record UpdateBookSummarySituationOperation(string Situation) : IOperation;

internal sealed class UpdateBookSummarySituationOperationHandler : IOperationHandler<UpdateBookSummarySituationOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IDataSession data;

    public UpdateBookSummarySituationOperationHandler(IProjectSessionHandler projectSessionHandler, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateBookSummarySituationOperation request, CancellationToken cancellationToken)
    {
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return await data.Books.Get()
            .ThenAsync(async (book, ct) =>
            {
                book.Extra.Situation = request.Situation;
                data.Books.Update(book);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
