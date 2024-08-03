namespace StoryForge.Core.Utils;

public static class UtilExtension
{
    public static Result<T> ToResult<T>(this Option<T> option) =>
        option.Match(Result<T>.Ok, () => Result<T>.Fail("Data not available"));

    public static Option<T> ToOption<T>(this Result<T> result) =>
        result.Match(Option<T>.Some, _ => Option<T>.None());
}
