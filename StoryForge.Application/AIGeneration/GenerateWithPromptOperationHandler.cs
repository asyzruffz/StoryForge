using StoryForge.Application.Abstractions;
using StoryForge.Core.Utils;

namespace StoryForge.Application.AIGeneration
{
    internal sealed class GenerateWithPromptOperationHandler
        : IOperationHandler<GenerateWithPromptOperation, string>
    {
        public GenerateWithPromptOperationHandler()
        {
            // dependency injection
        }

        public async Task<Result<string>> Handle(GenerateWithPromptOperation request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return await Task.FromResult(Result<string>.Fail("Prompt is empty"));

            var generated = $"This is an AI genrated response from the prompt: \"{request.Prompt}\"";
            return await Task.FromResult(Result<string>.Ok(generated));
        }
    }
}
