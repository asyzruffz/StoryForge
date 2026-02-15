using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record UpdateAuthorOperation(Author Author) : IOperation;

internal sealed class UpdateAuthorOperationHandler : IOperationHandler<UpdateAuthorOperation>
{
    private readonly IApplicationDataSession data;

    public UpdateAuthorOperationHandler(IApplicationDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateAuthorOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var projectId = ProjectId.Empty; // TODO: Get the current project id from a service
        data.Projects.GetById(projectId)
            .Then(project => project.Author = request.Author);
        data.Save();
        return Result.Ok();
    }
}
