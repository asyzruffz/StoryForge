namespace StoryForge.Core.Data;

public enum Importance
{
    Minor,
    Secondary,
    Main,
}

public static class ImportanceExtension
{
    public static int Tier(this Importance importance) => (int)importance;
}
