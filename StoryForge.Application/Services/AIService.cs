using StoryForge.Application.Abstractions;
using StoryForge.Core.Services;
using System.Text;

namespace StoryForge.Application.Services;

public class AIService : IAIService
{
    private readonly IOpenAIClient client;

    public AIService(IOpenAIClient openAIClient)
    {
        client = openAIClient;
    }

    public async Task<string> Complete(string preface, CancellationToken cancellationToken)
    {
        var result = await client.Complete(preface, cancellationToken);
        return result.Match(
            parts => parts.Aggregate(
                new StringBuilder(),
                (builder, next) => builder.AppendLine(next),
                builder => builder.ToString()), 
            error => error);
    }
}
