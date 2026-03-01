using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Summaries;

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

    public async Task<Result> Handle(UpdateBookSummarySituationOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return data.Books.Get()
            .Then(book =>
            {
                book.Extra.Situation = request.Situation;
                data.Books.Update(book);
                data.Save();
                return Result.Ok();
            });
    }
}
