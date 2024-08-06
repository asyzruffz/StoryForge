using Microsoft.Extensions.DependencyInjection;

namespace StoryForge.Simulator.Utils.Commands;

public class CommandProcessor : ICommandProcessor
{
    private readonly IServiceScopeFactory scopeFactory;

    public bool IsRunning { get; private set; } = true;

    public CommandProcessor(IServiceScopeFactory serviceScopeFactory) =>
        scopeFactory = serviceScopeFactory;

    public async Task Read(string? input, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested) return;
        if (string.IsNullOrEmpty(input)) return;

        var command = new CommandData(CommandParams.Parse(input));

        if (IsExit(command)) return; // Add default exit command

        using (var scope = scopeFactory.CreateAsyncScope())
        {
            var commandService = scope.ServiceProvider.GetKeyedService<ICommand>(command.Name);
            if (commandService == null)
            {
                Console.WriteLine($"{Environment.NewLine}No command called {command.Name} exists{Environment.NewLine}");
                return;
            }

            await commandService.Action!.Invoke(command, cancellationToken);
        }
    }

    bool IsExit(CommandData command)
    {
        if (command.Name != "exit")
            return false;
        IsRunning = false;
        return true;
    }
}
