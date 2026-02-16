using StoryForge.Core.Data;

namespace StoryForge.Core.Repositories;

public interface IProjectRepository : IRepository<Project>, IQueryableById<Project, string>;
