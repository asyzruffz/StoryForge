using StoryForge.Core.Data;

namespace StoryForge.Core.Repositories;

public interface IPlotRepository : IRepository<Plot>, IQueryableById<Plot, PlotId>;
