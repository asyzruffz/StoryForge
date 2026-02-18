using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record OpenProjectOperation(string FileName, Stream FileStream) : IOperation;

internal sealed class OpenProjectOperationHandler : IOperationHandler<OpenProjectOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IProjectFileStorage fileStorage;
    private readonly IApplicationDataSession appData;

    public OpenProjectOperationHandler(IProjectSessionHandler projectSessionHandler, IProjectFileStorage projectFileStorage, IApplicationDataSession appDataSession)
    {
        projectSession = projectSessionHandler;
        fileStorage = projectFileStorage;
        appData = appDataSession;
    }

    public async Task<Result> Handle(OpenProjectOperation request, CancellationToken cancellationToken)
    {
        // Save incoming stream to disk
        var saveResult = await fileStorage.SaveProjectFileAsync(request.FileName, request.FileStream, cancellationToken);
        if (!saveResult.IsSuccess)
        {
            return Result.Fail(saveResult.ErrorMessage);
        }

        var fullPath = saveResult.Or(string.Empty);

        // Check if project already exists in application data
        var existing = appData.Projects.GetById(fullPath);

        return existing.Match(
            project => projectSession.StartSession(project),
            _ =>
            {
                var project = new Project
                {
                    FilePath = fullPath,
                    Name = Path.GetFileNameWithoutExtension(fullPath),
                };

                appData.Projects.Create(project);
                appData.Save();

                return projectSession.StartSession(project, true);
            });
    }
}
