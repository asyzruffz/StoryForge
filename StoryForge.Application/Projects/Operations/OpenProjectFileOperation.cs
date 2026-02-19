using MediatR;
using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects.Operations;

public sealed record OpenProjectFileOperation(string FileName, Stream FileStream) : IOperation;

internal sealed class OpenProjectFileOperationHandler : IOperationHandler<OpenProjectFileOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IProjectFileStorage fileStorage;
    private readonly IApplicationDataSession appData;
    private readonly ISender sender;

    public OpenProjectFileOperationHandler(IProjectSessionHandler projectSessionHandler, IProjectFileStorage projectFileStorage, IApplicationDataSession appDataSession, ISender sender)
    {
        projectSession = projectSessionHandler;
        fileStorage = projectFileStorage;
        appData = appDataSession;
        this.sender = sender;
    }

    public async Task<Result> Handle(OpenProjectFileOperation request, CancellationToken cancellationToken)
    {
        // Save incoming stream to disk
        var saveResult = await fileStorage.SaveProjectFileAsync(request.FileName, request.FileStream, cancellationToken);
        if (!saveResult.IsSuccess)
        {
            return Result.Fail(saveResult.ErrorMessage);
        }

        var fullPath = saveResult.Or(string.Empty);

        var projectResult = await sender.Send(new OpenProjectPathOperation(fullPath));
        if (projectResult.IsSuccess || !projectResult.ErrorMessage.StartsWith("No valid project"))
        {
            return projectResult;
        }

        var project = new Project
        {
            FilePath = fullPath,
            Name = Path.GetFileNameWithoutExtension(fullPath),
        };

        appData.Projects.Create(project);
        appData.Save();

        return projectSession.StartSession(project, true);
    }
}
