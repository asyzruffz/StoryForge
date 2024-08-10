using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using StoryForge.Application.Abstractions;

namespace StoryForge.Infrastructure.OpenAI;

public class OpenAIClient : IOpenAIClient
{
    private readonly IOptionsMonitor<OpenAIConfig> option;
    private ChatClient? client;

    public OpenAIClient(IOptionsMonitor<OpenAIConfig> optionMonitor)
    {
        option = optionMonitor;

        Reset();
    }

    public async Task<IEnumerable<string>> Complete(string message, CancellationToken cancellationToken)
    {
        var result = await client!.CompleteChatAsync([message], cancellationToken: cancellationToken);
        return result.Value.Content.Select(content => content.Text);
    }

    public void Reset()
    {
        var config = option.CurrentValue;

        Console.WriteLine($"Key: {config.Key}, Url: {config.Url}, Model: {config.Model}");

        client = new ChatClient(config.Model, config.Key,
            new OpenAIClientOptions { Endpoint = new Uri(config.Url) });
    }
}
