namespace StoryForge.Core.Data;

public interface ITypedId
{
}

public static class TypedId
{
    public static string Empty => string.Empty;
    public static string New() => Guid.NewGuid().ToString();

    public static string From(string id)
    {
        try
        {
            return Guid.Parse(id).ToString();
        }
        catch (Exception)
        {
            return Empty;
        }
    }
}

public interface IIdentifiable
{
    string IdString { get; }
}
