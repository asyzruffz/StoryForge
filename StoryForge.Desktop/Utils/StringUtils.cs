namespace StoryForge.Desktop.Utils;

internal static class StringUtils
{
    public static string EllipsisMiddle(this string text, int maxLength)
    {
        maxLength = Math.Min(maxLength, text.Length);
        if (text.Length <= 4) return "...";
        int startPartLength = (maxLength - 3) / 2;
        int endPartLength = maxLength - startPartLength - 3;
        return $"{text.Substring(0, startPartLength)}...{text.Substring(text.Length - endPartLength)}";
    }
}
