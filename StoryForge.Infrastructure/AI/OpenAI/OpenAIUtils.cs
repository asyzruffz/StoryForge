using StoryForge.Core.Utils;
using System.ClientModel;

namespace StoryForge.Infrastructure.AI.OpenAI;

internal static class OpenAIUtils
{
    public static Result<T> ToResult<T>(this ClientResult<T> result) =>
        Result<T>.Ok(result.Value);
}
