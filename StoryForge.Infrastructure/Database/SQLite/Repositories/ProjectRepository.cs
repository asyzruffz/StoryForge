using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Data;
using StoryForge.Core.Repositories;
using StoryForge.Core.Utils;

namespace StoryForge.Infrastructure.Database.SQLite.Repositories;

internal class ProjectRepository : IProjectRepository
{
    protected readonly DbSet<Project> projects;

    public ProjectRepository(ApplicationDbContext context)
    {
        projects = context.Projects;
    }

    public IQueryable<Project> GetAll()
    {
        return projects.AsQueryable();
    }

    public Result<Project> GetById(ProjectId id)
    {
        return projects
            .SingleOrDefault(project => project.Id == id)
            .AsOption().ToResult();
    }

    public void Create(Project project)
    {
        projects.Add(project);
    }

    public void Create(IEnumerable<Project> project)
    {
        projects.AddRange(project);
    }

    public void Update(Project project)
    {
        projects.Update(project);
    }

    public void Delete(Project project)
    {
        projects.Remove(project);
    }
}
