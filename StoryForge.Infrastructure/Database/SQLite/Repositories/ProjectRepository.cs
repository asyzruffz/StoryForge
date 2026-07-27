using Keystone;
using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Data;
using StoryForge.Core.Projects;
using StoryForge.Core.Storage.Repositories;

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

    public Option<Project> GetById(string filePath)
    {
        return projects
            .SingleOrDefault(project => project.FilePath == filePath)
            .AsOption();
    }

    public bool HasWithId(string filePath) => projects.Find(filePath) != null;

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
