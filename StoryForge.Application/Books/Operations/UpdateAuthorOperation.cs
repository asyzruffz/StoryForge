using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Books.Operations;

public sealed record UpdateAuthorOperation(Author Author) : IOperation;

internal sealed class UpdateAuthorOperationHandler : IOperationHandler<UpdateAuthorOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IDataSession data;

    public UpdateAuthorOperationHandler(IProjectSessionHandler projectSessionHandler, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateAuthorOperation request, CancellationToken cancellationToken)
    {
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        data.Authors.Update(request.Author);
        await data.SaveAsync(cancellationToken).ConfigureAwait(false);
        return Result.Ok();
    }
}
