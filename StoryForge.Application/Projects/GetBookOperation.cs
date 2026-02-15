using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record GetBookOperation() : IOperation<Book>;

internal sealed class GetBookOperationHandler : IOperationHandler<GetBookOperation, Book>
{
    private readonly IApplicationDataSession data;

    public GetBookOperationHandler(IApplicationDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result<Book>> Handle(GetBookOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var projectId = ProjectId.Empty; // TODO: Get the current project id from a service
        var result = data.Projects.GetById(projectId)
            .Then(project => Result<Book>.Ok(project.Book));
        return result;
    }
}
