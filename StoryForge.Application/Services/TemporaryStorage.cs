using StoryForge.Core.Storage;
using StoryForge.Core.Utils;

namespace StoryForge.Application.Services;

public class TemporaryStorage : ITemporaryStorage
{
    private readonly Dictionary<string, object> storage = new();

    public void Set<T>(string key, T value) where T : notnull
    {
        storage[key] = value;
    }

    public Option<T> Get<T>(string key)
    {
        bool success = storage.TryGetValue(key, out var value);
        return success ? Option<T>.Some((T)value!) : Option<T>.None();
    }

    public T Get<T>(string key, T defaultValue)
    {
        bool success = storage.TryGetValue(key, out var value);
        return success ? (T)value! : defaultValue;
    }

    public void Delete(string key)
    {
        storage.Remove(key);
    }

    public void Clear()
    {
        storage.Clear();
    }
}
