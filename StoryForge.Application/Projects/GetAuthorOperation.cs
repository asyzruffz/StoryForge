using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record GetAuthorOperation : IOperation<Author>;

internal sealed class GetAuthorOperationHandler : IOperationHandler<GetAuthorOperation, Author>
{
    private readonly IApplicationDataSession data;

    public GetAuthorOperationHandler(IApplicationDataSession dataSession)
    {
        data = dataSession;
    }

    public async Task<Result<Author>> Handle(GetAuthorOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        var projectId = ProjectId.Empty; // TODO: Get the current project id from a service
        var result = data.Projects.GetById(projectId)
            .Then(project => Result<Author>.Ok(project.Author));
        return result;
    }
}
