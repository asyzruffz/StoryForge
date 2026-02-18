using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Projects;

public sealed record GetAuthorOperation : IOperation<Author>;

internal sealed class GetAuthorOperationHandler : IOperationHandler<GetAuthorOperation, Author>
{
    private readonly IProjectSessionHandler projectSession;
    private readonly IDataSession data;

    public GetAuthorOperationHandler(IProjectSessionHandler projectSessionHandler, IDataSession dataSession)
    {
        projectSession = projectSessionHandler;
        data = dataSession;
    }

    public async Task<Result<Author>> Handle(GetAuthorOperation request, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        if (!projectSession.IsActive)
        {
            return Result<Author>.Fail("No project is open");
        }

        return data.Authors.Get();
    }
}
