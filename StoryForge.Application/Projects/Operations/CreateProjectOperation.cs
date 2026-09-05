using Keystone;
using Keystone.Application;
using StoryForge.Core.Projects;

namespace StoryForge.Application.Projects.Operations;

public sealed record CreateProjectOperation(string Name, string FilePath) : IOperation;

internal sealed class CreateProjectOperationHandler : IOperationHandler<CreateProjectOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IProjectFileStorage fileStorage;

    public CreateProjectOperationHandler(IProjectSessionHandler projectSessionHandler, IProjectFileStorage projectFileStorage)
    {
        projectSession = projectSessionHandler;
        fileStorage = projectFileStorage;
    }

    public async ValueTask<Result> Handle(CreateProjectOperation request, CancellationToken cancellationToken)
    {
        var filePath = request.FilePath;
        if (string.IsNullOrWhiteSpace(request.FilePath))
        {
            filePath = fileStorage.CreateProjectPath(request.Name);
        }

        Project newProject = new Project
        {
            FilePath = filePath,
            Name = request.Name,
        };

        return await projectSession
            .StartSession(newProject, cancellationToken)
            .ConfigureAwait(false);
    }
}
