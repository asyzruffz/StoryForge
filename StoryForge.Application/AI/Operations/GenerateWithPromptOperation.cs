using StoryForge.Application.Abstractions;
using StoryForge.Core.AI.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.AI.Operations;

public sealed record GenerateWithPromptOperation(string Prompt) : IOperation<string>;

internal sealed class GenerateWithPromptOperationHandler
    : IOperationHandler<GenerateWithPromptOperation, string>
{
    private readonly IAIService ai;

    public GenerateWithPromptOperationHandler(IAIService aiService)
    {
        ai = aiService;
    }

    public async Task<Result<string>> Handle(GenerateWithPromptOperation request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
            return await Task.FromResult(Result<string>.Fail("Prompt is empty"));

        //var generated = $"This is an AI generated response from the prompt: \"{request.Prompt}\"";
        var generated = await ai.Complete(request.Prompt, cancellationToken);
        return await Task.FromResult(Result<string>.Ok(generated));
    }
}
