using Keystone;
using Keystone.Application;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage;

namespace StoryForge.Application.Projects.Operations;

public record GetRecentProjectsOperation : IOperation<IEnumerable<Project>>;

internal sealed class GetRecentProjectsOperationHandler : IOperationHandler<GetRecentProjectsOperation, IEnumerable<Project>>
{
    private readonly IApplicationDataSession appData;

    public GetRecentProjectsOperationHandler(IApplicationDataSession applicationDataSession)
    {
        appData = applicationDataSession;
    }

    public async ValueTask<Result<IEnumerable<Project>>> Handle(GetRecentProjectsOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var projects = appData.Projects.GetAll()
            .OrderByDescending(project => project.LastActive)
            .Take(10);
        return Result<IEnumerable<Project>>.Ok(projects);
    }
}
