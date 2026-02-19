namespace StoryForge.Core.AI.Chat;

public interface IStreamingObserver
{
    Task OnPartialAsync(string partial, CancellationToken cancellationToken = default);
    Task OnCompleteAsync(CancellationToken cancellationToken = default);
    Task OnErrorAsync(string error, CancellationToken cancellationToken = default);
}
