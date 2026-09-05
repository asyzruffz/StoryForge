using Keystone;
using Keystone.Application;
using StoryForge.Core.AI.Services;

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

    public async ValueTask<Result<string>> Handle(GenerateWithPromptOperation request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Prompt))
            return Result<string>.Fail("Prompt is empty");

        //var generated = $"This is an AI generated response from the prompt: \"{request.Prompt}\"";
        var generated = await ai.Complete(request.Prompt, cancellationToken);
        return Result<string>.Ok(generated);
    }
}
