using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record GetBookOperation() : IOperation<Book>;

internal sealed class GetBookOperationHandler : IOperationHandler<GetBookOperation, Book>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IApplicationDataSession data;

    public GetBookOperationHandler(IProjectSessionHandler projectSessionHandler, IApplicationDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async Task<Result<Book>> Handle(GetBookOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result<Book>.Fail("No project is open");
        }

        var projectId = projectSession.CurrentProject!;
        var result = data.Projects.GetById(projectId)
            .Then(project => Result<Book>.Ok(project.Book));
        return result;
    }
}
