using Keystone;
using Keystone.Application;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Books.Operations;

public sealed record UpdateAuthorEmailOperation(string? Email) : IOperation;

internal sealed class UpdateAuthorEmailOperationHandler : IOperationHandler<UpdateAuthorEmailOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IDataSession data;

    public UpdateAuthorEmailOperationHandler(IProjectSessionHandler projectSessionHandler, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async ValueTask<Result> Handle(UpdateAuthorEmailOperation request, CancellationToken cancellationToken)
    {
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return await data.Authors.Get()
            .ThenAsync(async (author, ct) =>
            {
                author.Email = request.Email;
                data.Authors.Update(author);
                await data.SaveAsync(ct).ConfigureAwait(false);
                return Result.Ok();
            }, cancellationToken)
            .ConfigureAwait(false);
    }
}
