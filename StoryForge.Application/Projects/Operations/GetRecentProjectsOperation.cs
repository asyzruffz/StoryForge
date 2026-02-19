using StoryForge.Application.Abstractions;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects.Operations;

public record GetRecentProjectsOperation : IOperation<IEnumerable<Project>>;

internal sealed class GetRecentProjectsOperationHandler : IOperationHandler<GetRecentProjectsOperation, IEnumerable<Project>>
{
    private readonly IApplicationDataSession appData;

    public GetRecentProjectsOperationHandler(IApplicationDataSession applicationDataSession)
    {
        appData = applicationDataSession;
    }

    public async Task<Result<IEnumerable<Project>>> Handle(GetRecentProjectsOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var projects = appData.Projects.GetAll()
            .OrderByDescending(project => project.LastActive)
            .Take(10);
        return Result<IEnumerable<Project>>.Ok(projects);
    }
}
