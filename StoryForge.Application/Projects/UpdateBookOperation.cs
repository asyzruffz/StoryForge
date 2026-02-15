using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record UpdateBookOperation(Book Book) : IOperation;

internal sealed class UpdateBookOperationHandler : IOperationHandler<UpdateBookOperation>
{
    private readonly IApplicationDataSession data;

    public UpdateBookOperationHandler(IApplicationDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateBookOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var projectId = ProjectId.Empty; // TODO: Get the current project id from a service
        data.Projects.GetById(projectId)
            .Then(project => project.Book = request.Book);
        data.Save();
        return Result.Ok();
    }
}
