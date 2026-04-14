namespace StoryForge.MudPresentation.Utils;

internal static class WordCounter
{
    public static int WordCount(this string text)
    {
        var words = text.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
        return words.Count();
    }
}
