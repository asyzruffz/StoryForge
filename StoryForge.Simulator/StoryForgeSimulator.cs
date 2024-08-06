using Microsoft.Extensions.Hosting;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator;

public class StoryForgeSimulator : BackgroundService
{
    private readonly IHostApplicationLifetime simulation;
    private readonly ICommandProcessor process;

    public StoryForgeSimulator(IHostApplicationLifetime appLifetime, ICommandProcessor commandProcessor)
    {
        simulation = appLifetime;
        process = commandProcessor;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Console.WriteLine("Story Forge");
        while (process.IsRunning && !stoppingToken.IsCancellationRequested)
        {
            Console.Write(">> ");
            var input = Console.ReadLine();

            await process.Read(input, stoppingToken);
        }
        simulation.StopApplication();
    }
}
