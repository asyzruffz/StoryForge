using MediatR;
using Microsoft.Extensions.Hosting;

namespace StoryForge.Simulator.Utils.Commands;

public abstract class CommandBase : ICommand
{
    protected readonly ISender Sender;

    protected CommandBase(ISender sender) => Sender = sender;

    public abstract string Name { get; }
    public abstract Func<List<string>, Task> Action { get; }

    protected bool HasNoArgument(IEnumerable<string> args)
    {
        if (args.Count() == 0) return true;
        Console.WriteLine($"No argument for {Name} provided{Environment.NewLine}");
        return false;
    }

    protected void UnknownArgument(string arg)
    {
        Console.WriteLine($"Unknown argument {arg} for {Name}{Environment.NewLine}");
    }

    protected void Message(string msg)
    {
        Console.WriteLine($"{msg}{Environment.NewLine}");
    }
}
