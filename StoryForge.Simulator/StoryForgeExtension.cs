using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace StoryForge.Simulator;

public static class StoryForgeExtension
{
    public static IHostBuilder SetNullLogger(this IHostBuilder builder) => builder
        .ConfigureLogging(logging => logging
            .ClearProviders()
            .AddProvider(NullLoggerProvider.Instance));
}
