using Microsoft.Extensions.Hosting;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator;

public class StoryForgeSimulator : IHostedService
{
    private readonly IHostApplicationLifetime simulation;
    private readonly CommandProcessor process;

    public StoryForgeSimulator(IHostApplicationLifetime appLifetime, CommandProcessor commandProcessor)
    {
        simulation = appLifetime;
        process = commandProcessor;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Story Forge");
        return Run();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Exiting Simulator");
        return Task.CompletedTask;
    }

    private async Task Run()
    {
        do
        {
            Console.Write(">> ");
            var input = Console.ReadLine();

            await process.Read(input);
        } while (process.IsRunning);
        simulation.StopApplication();
    }
}
