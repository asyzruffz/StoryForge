namespace StoryForge.Simulator.Utils.Commands;

public interface ICommand
{
    public string Name { get; }
    public Func<List<string>, Task> Action { get; }
}
