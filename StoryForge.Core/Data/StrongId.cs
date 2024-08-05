using StoryForge.Core.Utils;

namespace StoryForge.Core.Data;

public interface IStrongId { }

public static class StrongId
{
    public static string New() => Guid.NewGuid().ToString();

    public static Option<string> From(string id)
    {
        try
        {
            return Option<string>.Some(Guid.Parse(id).ToString());
        }
        catch (Exception)
        {
            return Option<string>.None();
        }
    }
}