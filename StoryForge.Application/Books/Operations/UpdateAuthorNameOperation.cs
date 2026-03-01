using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

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

    public async Task<Result> Handle(UpdateAuthorNameOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        return data.Authors.Get()
            .Then(author =>
            {
                author.Name = request.Name;
                data.Authors.Update(author);
                data.Save();
                return Result.Ok();
            });
    }
}
