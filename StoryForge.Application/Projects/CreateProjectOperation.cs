using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record CreateProjectOperation(string Name) : IOperation;

internal sealed class CreateProjectOperationHandler : IOperationHandler<CreateProjectOperation>
{
    private readonly IApplicationDataSession appData;
    private readonly IDataSession data;

    public CreateProjectOperationHandler(IApplicationDataSession appDataSession, IDataSession dataSession)
    {
        appData = appDataSession;
        data = dataSession;
    }

    public async Task<Result> Handle(CreateProjectOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var extra = BookSummary.New();

        Project newProject = new Project
        {
            Id = ProjectId.New(),
            Book = new Book
            {
                Title = request.Name,
                Extra = extra
            },
            Author = new Author()
        };

        appData.Projects.Create(newProject);
        data.Summaries.Create(extra.Summary);

        appData.Save();
        data.Save();
        return Result.Ok();
    }
}
