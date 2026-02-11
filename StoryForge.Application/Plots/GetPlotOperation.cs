using StoryForge.Application.Abstractions;

namespace StoryForge.Application.Plots;

public sealed record GetPlotOperation(string Name) : IOperation;
