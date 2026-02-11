using StoryForge.Application.Abstractions;
using StoryForge.Core.Data;

namespace StoryForge.Application.Plots;

public sealed record GetPlotEntriesOperation : IOperation<IEnumerable<PlotEntry>>;
