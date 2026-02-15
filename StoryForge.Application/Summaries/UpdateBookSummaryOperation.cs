using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Summaries;

public sealed record UpdateBookSummaryOperation(BookSummary Summary) : IOperation;

internal sealed class UpdateBookSummaryOperationHandler : IOperationHandler<UpdateBookSummaryOperation>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IApplicationDataSession appData;
    private readonly IDataSession data;

    public UpdateBookSummaryOperationHandler(IProjectSessionHandler projectSessionHandler, IApplicationDataSession appDataSession, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        appData = appDataSession;
        data = dataSession;
    }

    public async Task<Result> Handle(UpdateBookSummaryOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result.Fail("No project is open");
        }

        var projectId = projectSession.CurrentProject!;

        var projectResult = appData.Projects.GetById(projectId);
        if (!projectResult.IsSuccess)
        {
            return Result.Fail(projectResult.ErrorMessage);
        }

        projectResult.Then(project => project.Book.Extra = request.Summary);
        data.Summaries.Update(request.Summary.Summary);

        appData.Save();
        data.Save();
        return Result.Ok();
    }
}
