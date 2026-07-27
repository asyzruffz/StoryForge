using Keystone;
using Keystone.Application;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;

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

    public async ValueTask<Result> Handle(OpenProjectFileOperation request, CancellationToken cancellationToken)
    {
        // Save incoming stream to disk
        var saveResult = await fileStorage
            .SaveProjectFileAsync(request.FileName, request.FileStream, cancellationToken)
            .ConfigureAwait(false);

        return await saveResult
            .ThenAsync(async (fullPath, ct) => 
                await sender.Send(new OpenProjectPathOperation(fullPath), ct),
                cancellationToken)
            .ConfigureAwait(false);
    }
}
