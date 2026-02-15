using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record UpdateBookOperation(Book Book) : IOperation;

internal sealed class UpdateBookOperationHandler : IOperationHandler<UpdateBookOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IApplicationDataSession data;

    public UpdateBookOperationHandler(IProjectSessionHandler projectSessionHandler, IApplicationDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateBookOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        var projectId = projectSession.CurrentProject!;
        data.Projects.GetById(projectId)
            .Then(project => project.Book = request.Book);
        data.Save();
        return Result.Ok();
    }
}
