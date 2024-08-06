using MediatR;
using StoryForge.Application.AIGeneration;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator.Commands;

public class TestCommand : CommandBase
{
    public const string Name = "test";

    public override Func<CommandData, CancellationToken, Task> Action => Execute;

    public TestCommand(ISender sender) : base(sender) { }

    private async Task Execute(CommandData command, CancellationToken cancellationToken)
    {
        if (!command.ParamIsAtLeast(1))
        {
            command.NoArgument();
            return;
        }

        var subcommand = new CommandData(command.Params);
        switch (subcommand.Name)
        {
            case "gen": await GenerateText(subcommand, cancellationToken); break;
            case "weight": break;
            default: command.UnknownArgument(subcommand.Name); break;
        }

        await Task.CompletedTask;
    }

    private async Task GenerateText(CommandData command, CancellationToken cancellationToken)
    {
        if (!command.ParamIsAtLeast(1))
        {
            command.NoArgument();
            return;
        }

        Message(string.Empty);
        var result = await Sender.Send(new GenerateWithPromptOperation(command.Params[0]));
        result.OnError(MessageLine).Then(MessageLine);
    }
}
