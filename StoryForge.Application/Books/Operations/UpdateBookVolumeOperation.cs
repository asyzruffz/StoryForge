using Keystone;
using Keystone.Application;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Books.Operations;

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

    public async ValueTask<Result> Handle(UpdateBookVolumeOperation request, CancellationToken cancellationToken)
    {
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return await data.Books.Get()
            .ThenAsync(async (book, ct) =>
            {
                book.Volume = request.Volume;
                data.Books.Update(book);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
