using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage.Repositories;
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

    public Result<Project> GetById(string filePath)
    {
        return projects
            .SingleOrDefault(project => project.FilePath == filePath)
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
