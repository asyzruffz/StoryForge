using StoryForge.Core.Data;

namespace StoryForge.Core.Storage.Repositories;

public interface IProjectRepository : IRepository<Project>, IQueryableById<Project, string>;
