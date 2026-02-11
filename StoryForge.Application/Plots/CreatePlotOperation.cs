using StoryForge.Application.Abstractions;

namespace StoryForge.Application.Plots;

public sealed record CreatePlotOperation(string Name) : IOperation;
