using MediatR;

namespace StoryForge.Simulator.Utils.Commands;

public abstract class CommandBase : ICommand
{
    protected readonly ISender Sender;

    protected CommandBase(ISender sender) => Sender = sender;

    public abstract Func<CommandData, CancellationToken, Task> Action { get; }

    protected void Message(string msg)
    {
        Console.WriteLine(msg);
    }

    protected void MessageLine(string msg)
    {
        Console.WriteLine($"{msg}{Environment.NewLine}");
    }
}
