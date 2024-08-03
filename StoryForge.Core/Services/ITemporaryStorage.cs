using StoryForge.Core.Utils;

namespace StoryForge.Core.Services;

public interface ITemporaryStorage
{
    void Set<T>(string key, T value) where T : notnull;
    Option<T> Get<T>(string key);
    T Get<T>(string key, T defaultValue);
    void Delete(string key);
    void Clear();
}
