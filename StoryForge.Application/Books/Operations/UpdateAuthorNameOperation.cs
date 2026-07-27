using Keystone;
using Keystone.Application;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Books.Operations;

public sealed record UpdateAuthorNameOperation(string Name) : IOperation;

internal sealed class UpdateAuthorNameOperationHandler : IOperationHandler<UpdateAuthorNameOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IDataSession data;

    public UpdateAuthorNameOperationHandler(IProjectSessionHandler projectSessionHandler, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateAuthorNameOperation request, CancellationToken cancellationToken)
    {
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return await data.Authors.Get()
            .ThenAsync(async (author, ct) =>
            {
                author.Name = request.Name;
                data.Authors.Update(author);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
