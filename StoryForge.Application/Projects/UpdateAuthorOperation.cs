using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record UpdateAuthorOperation(Author Author) : IOperation;

internal sealed class UpdateAuthorOperationHandler : IOperationHandler<UpdateAuthorOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IApplicationDataSession data;

    public UpdateAuthorOperationHandler(IProjectSessionHandler projectSessionHandler, IApplicationDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateAuthorOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        var projectId = projectSession.CurrentProject!;
        data.Projects.GetById(projectId)
            .Then(project => project.Author = request.Author);
        data.Save();
        return Result.Ok();
    }
}
