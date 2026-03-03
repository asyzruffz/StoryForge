using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects.Operations;

public sealed record CreateProjectOperation(string Name) : IOperation;

internal sealed class CreateProjectOperationHandler : IOperationHandler<CreateProjectOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IProjectFileStorage fileStorage;

    public CreateProjectOperationHandler(IProjectSessionHandler projectSessionHandler, IProjectFileStorage projectFileStorage)
    {
        projectSession = projectSessionHandler;
        fileStorage = projectFileStorage;
    }

    public async Task<Result> Handle(CreateProjectOperation request, CancellationToken cancellationToken)
    {
        var filePath = fileStorage.CreateProjectPath(request.Name);

        Project newProject = new Project
        {
            FilePath = filePath,
            Name = request.Name,
        };

        return await projectSession
            .StartSession(newProject, true, cancellationToken)
            .ConfigureAwait(false);
    }
}
