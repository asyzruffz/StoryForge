using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Books;

public sealed record UpdateBookVolumeOperation(string Volume) : IOperation;

internal sealed class UpdateBookVolumeOperationHandler : IOperationHandler<UpdateBookVolumeOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IDataSession data;

    public UpdateBookVolumeOperationHandler(IProjectSessionHandler projectSessionHandler, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateBookVolumeOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return data.Books.Get()
            .Then(book =>
            {
                book.Volume = request.Volume;
                data.Books.Update(book);
                data.Save();
                return Result.Ok();
            });
    }
}
