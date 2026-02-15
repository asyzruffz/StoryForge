using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Summaries;

public sealed record GetBookSummaryOperation : IOperation<BookSummary>;

internal sealed class GetBookSummaryOperationHandler : IOperationHandler<GetBookSummaryOperation, BookSummary>
{
    private readonly IApplicationDataSession appData;
    private readonly IDataSession data;

    public GetBookSummaryOperationHandler(IApplicationDataSession appDataSession, IDataSession dataSession)
    {
        appData = appDataSession;
        data = dataSession;
    }

    public async Task<Result<BookSummary>> Handle(GetBookSummaryOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var projectId = ProjectId.Empty; // TODO: Get the current project id from a service

        return appData.Projects.GetById(projectId)
            .Then(project =>
            {
                var extra = project.Book.Extra;

                var summaryResult = data.Summaries.GetById(extra.SummaryId);
                if (!summaryResult.IsSuccess)
                {
                    return Result<BookSummary>.Fail(summaryResult.ErrorMessage);
                }
                summaryResult.Then(summary => extra.Summary = summary);

                return Result<BookSummary>.Ok(extra);
            });
    }
}
