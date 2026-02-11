using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;
using StoryForge.Core.Utils;

namespace StoryForge.Application.About;

public sealed record GetAppInfoOperation : IOperation<AppInfo>;

internal sealed class GetAppInfoOperationHandler : IOperationHandler<GetAppInfoOperation, AppInfo>
{
    public Task<Result<AppInfo>> Handle(GetAppInfoOperation request, CancellationToken cancellationToken)
    {
        // TODO: Replace with actual implementation that fetches version from assembly,
        // build info from configuration, etc.
        var appInfo = AppInfo.Default;
        return Task.FromResult(Result<AppInfo>.Ok(appInfo));
    }
}
