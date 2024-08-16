using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using StoryForge.Application.Abstractions;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.OpenAI;

public class OpenAIClient : IOpenAIClient
{
    private readonly IOptionsMonitor<OpenAIConfig> option;
    private ChatClient client = null!;

    public OpenAIClient(IOptionsMonitor<OpenAIConfig> optionMonitor)
    {
        option = optionMonitor;

        Reset();
    }

    public async Task<Result<IEnumerable<string>>> Complete(string message, CancellationToken cancellationToken)
    {
        try
        {
            var clientResult = await client.CompleteChatAsync([message], cancellationToken: cancellationToken);
            return clientResult.ToResult()
                .Then(completion => Result<IEnumerable<string>>
                    .Ok(completion.Content.Select(content => content.Text)));
        }
        catch (Exception ex)
        {
            return Result<IEnumerable<string>>.Fail(ex.Message);
        }
    }

    public void Reset()
    {
        var config = option.CurrentValue;
        //Console.WriteLine($"Key: {config.Key}, Url: {config.Url}, Model: {config.Model}");

        client = new ChatClient(config.Model, config.Key,
            new OpenAIClientOptions { Endpoint = new Uri(config.Url) });
    }
}
