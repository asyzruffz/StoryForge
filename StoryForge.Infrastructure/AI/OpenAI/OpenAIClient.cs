using Microsoft.Extensions.Options;
using OpenAI;
using OpenAI.Chat;
using StoryForge.Core.AI.Auxiliaries;
using StoryForge.Core.AI.Chat;
using StoryForge.Core.AI.Providers;
using StoryForge.Core.Utils;
using System.ClientModel;
using System.Text;

namespace StoryForge.Infrastructure.AI.OpenAI;

public class OpenAIClient : ILLMClient
{
    private readonly IOptionsMonitor<OpenAIConfig> option;
    private ChatClient client = null!;

    public string ProviderId { get; init; } = "OpenAI";
    public string ModelId { get; private set; }

    public OpenAIClient(IOptionsMonitor<OpenAIConfig> optionMonitor)
    {
        option = optionMonitor;
        ModelId = option.CurrentValue.Model;

        Reset();
    }

    public void Reset()
    {
        var config = option.CurrentValue;
        //Console.WriteLine($"Key: {config.Key}, Url: {config.Url}, Model: {config.Model}");

        ModelId = config.Model;
        client = new ChatClient(config.Model, new ApiKeyCredential(config.Key),
            new OpenAIClientOptions { Endpoint = new Uri(config.Url) });
    }

    public async Task<Result<Completion>> CompleteAsync(IReadOnlyList<Message> messages, CancellationToken cancellationToken = default)
    {
        try
        {
            var msgs = messages.Select(msg => msg.ToOpenAIMessage());
            var clientResult = await client.CompleteChatAsync(msgs, cancellationToken: cancellationToken);
            return clientResult.ToResult()
                .Then(completion => Result<string>.Ok(completion.Content
                    .Aggregate(new StringBuilder(),
                        (builder, content) => builder.Append(content.Text)).ToString()))
                .Then(completion => Result<Completion>.Ok(new Completion(completion)));
        }
        catch (Exception ex)
        {
            return Result<Completion>.Fail(ex.Message);
        }
    }

    public Task<Result<Embedding>> GetEmbeddingsAsync(IReadOnlyList<string> inputs, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ModelInfo> GetModelInfoAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task StreamCompleteAsync(IReadOnlyList<Message> messages, IStreamingObserver observer, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
