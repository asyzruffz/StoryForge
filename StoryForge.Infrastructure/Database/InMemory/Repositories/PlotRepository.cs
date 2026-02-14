using StoryForge.Core.Data;
using StoryForge.Core.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.InMemory.Repositories;

internal class PlotRepository : IPlotRepository
{
    protected readonly List<Plot> plots;

    public PlotRepository(ProjectDbContext context)
    {
        plots = context.Plots;
    }

    public IQueryable<Plot> GetAll()
    {
        return plots.AsQueryable();
    }

    public Result<Plot> GetById(PlotId id)
    {
        return plots
            .SingleOrDefault(plot => plot.Id == id)
            .AsOption().ToResult();
    }

    public void Create(Plot plot)
    {
        plots.Add(plot);
    }

    public void Create(IEnumerable<Plot> plot)
    {
        plots.AddRange(plot);
    }

    public void Update(Plot plot)
    {
        var foundPlot = plots.SingleOrDefault(entry => entry.Id == plot.Id);
        if (foundPlot is null) return;

        int idx = plots.IndexOf(foundPlot);
        plots[idx] = plot;
    }

    public void Delete(Plot plot)
    {
        plots.Remove(plot);
    }
}
