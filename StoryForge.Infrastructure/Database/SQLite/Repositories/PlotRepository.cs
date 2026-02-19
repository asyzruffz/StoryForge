using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Data;
using StoryForge.Core.Storage.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.SQLite.Repositories;

internal class PlotRepository : IPlotRepository
{
    protected readonly DbSet<Plot> plots;

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
        plots.Update(plot);
    }

    public void Delete(Plot plot)
    {
        plots.Remove(plot);
    }
}
