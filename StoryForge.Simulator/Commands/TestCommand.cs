using MediatR;
using StoryForge.Application.AIGeneration;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator.Commands;

public class TestCommand : CommandBase
{
    public override string Name => "test";
    public override Func<List<string>, Task> Action => Execute;

    public TestCommand(ISender sender) : base(sender) { }

    private async Task Execute(List<string> param)
    {
        var subcommand = new CommandData(param);
        if (HasNoArgument(param)) return;

        switch (subcommand.Name)
        {
            case "gen": await GenerateText(subcommand); break;
            case "weight": break;
            default: UnknownArgument(subcommand.Name); break;
        }

        await Task.CompletedTask;
    }

    private async Task GenerateText(CommandData command)
    {
        if (!command.ParamIsAtLeast(1)) return;

        Message(string.Empty);
        var result = await Sender.Send(new GenerateWithPromptOperation(command.Params[0]));
        Message(result);
    }
}
