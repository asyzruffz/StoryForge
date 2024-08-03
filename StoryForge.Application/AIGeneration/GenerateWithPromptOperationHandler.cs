using StoryForge.Application.Abstractions;

namespace StoryForge.Application.AIGeneration
{
    internal sealed class GenerateWithPromptOperationHandler
        : IOperationHandler<GenerateWithPromptOperation, string>
    {
        public GenerateWithPromptOperationHandler()
        {
            // dependency injection
        }

        public async Task<string> Handle(GenerateWithPromptOperation request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Prompt))
                return await Task.FromResult(string.Empty);

            return await Task.FromResult($"This is an AI genrated response from the prompt: \"{request.Prompt}\"");
        }
    }
}
