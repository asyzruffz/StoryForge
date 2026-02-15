using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record CreateProjectOperation(string Name, string FilePath) : IOperation;

internal sealed class CreateProjectOperationHandler : IOperationHandler<CreateProjectOperation>
{
    private readonly IApplicationDataSession appData;
    private readonly IProjectSessionHandler projectSession;

    public CreateProjectOperationHandler(IApplicationDataSession appDataSession, IProjectSessionHandler projectSessionHandler)
    {
        appData = appDataSession;
        projectSession = projectSessionHandler;
    }

    public async Task<Result> Handle(CreateProjectOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        Project newProject = new Project
        {
            Id = ProjectId.New(),
            FilePath = request.FilePath,
            Book = new Book
            {
                Title = request.Name,
                Extra = BookSummary.New(),
            },
            Author = new Author()
        };

        appData.Projects.Create(newProject);
        appData.Save();

        return projectSession
            .StartSession(newProject);
    }
}
