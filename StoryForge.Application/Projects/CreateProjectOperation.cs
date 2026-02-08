using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record CreateProjectOperation(string Name) : IOperation;

internal sealed class CreateProjectOperationHandler : IOperationHandler<CreateProjectOperation>
{
    private readonly IDataSession data;

    public CreateProjectOperationHandler(IDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(CreateProjectOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        Book newBook = new Book
        {
            Title = request.Name,
            Extra = BookSummary.New()
        };

        data.Books.Update(newBook);
        data.Save();
        return Result.Ok();
    }
}
