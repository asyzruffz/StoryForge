using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

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

    public async Task<Result> Handle(UpdateBookSeriesOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return data.Books.Get()
            .Then(book =>
            {
                book.Series = request.Series;
                data.Books.Update(book);
                data.Save();
                return Result.Ok();
            });
    }
}
