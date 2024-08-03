using StoryForge.Application.Abstractions;

namespace StoryForge.Application.AIGeneration;

public sealed record GenerateWithPromptOperation(string Prompt) : IOperation<string>;
