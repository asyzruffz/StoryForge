using StoryForge.Core.Projects;
using StoryForge.Core.Utils;

namespace StoryForge.Core.Storage.Repositories;

public interface IProjectInfoRepository
{
    Result<string> Get(ProjectMeta category);
    void Set(ProjectMeta category, string value);
    void Delete(ProjectMeta category);
}
