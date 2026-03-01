using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

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

    public async Task<Result> Handle(UpdateAuthorEmailOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return data.Authors.Get()
            .Then(author =>
            {
                author.Email = request.Email;
                data.Authors.Update(author);
                data.Save();
                return Result.Ok();
            });
    }
}
