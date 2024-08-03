using StoryForge.Simulator.Commands;

namespace StoryForge.Simulator.Utils.Commands;

public class CommandProcessor
{
    public bool IsRunning { get; private set; } = true;

    Dictionary<string, Func<List<string>, Task>> commandTable = [];

    public CommandProcessor(
        VersionCommand versionCommand,
        TestCommand testCommand)
    {
        // Add default exit command
        Register("exit", async (_) =>
        {
            IsRunning = false;
            await Task.CompletedTask;
        });

        Register(versionCommand);
        Register(testCommand);
    }

    public CommandProcessor Register(ICommand command)
    {
        commandTable.Add(command.Name, command.Action);
        return this;
    }

    public CommandProcessor Register(string name, Func<List<string>, Task> command)
    {
        commandTable.Add(name, command);
        return this;
    }

    public async Task Read(string? input)
    {
        if (string.IsNullOrEmpty(input)) return;

        var command = new CommandData(input);
        if (commandTable.ContainsKey(command.Name))
        {
            await commandTable[command.Name]!.Invoke(command.Params);
        }
    }
}
