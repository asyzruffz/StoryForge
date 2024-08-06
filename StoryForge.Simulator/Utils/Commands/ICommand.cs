namespace StoryForge.Simulator.Utils.Commands;

public interface ICommand
{
    public Func<CommandData, CancellationToken, Task> Action { get; }
}
