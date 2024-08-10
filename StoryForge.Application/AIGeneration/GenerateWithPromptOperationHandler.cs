using StoryForge.Application.Abstractions;
using StoryForge.Core.Services;
using StoryForge.Core.Utils;

namespace StoryForge.Application.AIGeneration;

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
