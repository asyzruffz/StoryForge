using Keystone.Core;
using StoryForge.Core.Projects;

namespace StoryForge.Core.Storage.Repositories;

public interface IProjectRepository : IRepository<Project>, IQueryableById<Project, string>;
