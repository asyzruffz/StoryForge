using Keystone;
using Keystone.Application;
using StoryForge.Core.Data;

namespace StoryForge.Application.About;

public sealed record GetAppInfoOperation : IOperation<AppInfo>;

internal sealed class GetAppInfoOperationHandler : IOperationHandler<GetAppInfoOperation, AppInfo>
{
    public ValueTask<Result<AppInfo>> Handle(GetAppInfoOperation request, CancellationToken cancellationToken)
    {
        // TODO: Replace with actual implementation that fetches version from assembly,
        // build info from configuration, etc.
        var appInfo = AppInfo.Default;
        return ValueTask.FromResult(Result<AppInfo>.Ok(appInfo));
    }
}
