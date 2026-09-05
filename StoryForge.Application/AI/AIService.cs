using StoryForge.Core.AI.Chat;
using StoryForge.Core.AI.Providers;
using StoryForge.Core.AI.Services;

namespace StoryForge.Application.AI;

public class AIService : IAIService
{
    private readonly ILLMClient client;

    public AIService(ILLMClient openAIClient)
    {
        client = openAIClient;
    }

    public async Task<string> Complete(string preface, CancellationToken cancellationToken)
    {
        var result = await client.CompleteAsync([new Message("Complete", MessageRole.User, preface)], cancellationToken);
        return result.Match(completion => completion.Payload, error => error);
    }
}
