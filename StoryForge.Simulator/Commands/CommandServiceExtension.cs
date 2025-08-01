﻿using Microsoft.Extensions.DependencyInjection;
using StoryForge.Simulator.Utils.Commands;

namespace StoryForge.Simulator.Commands;

public static class CommandServiceExtension
{
    public static IServiceCollection AddCommandService(this IServiceCollection services)
    {
        services
            .AddKeyedScoped<ICommand, VersionCommand>(VersionCommand.Name)
            .AddKeyedScoped<ICommand, ClearCommand>(ClearCommand.Name)
            .AddKeyedScoped<ICommand, PromptCommand>(PromptCommand.Name)
            .AddKeyedScoped<ICommand, ChapterCommand>(ChapterCommand.Name)
            .AddKeyedScoped<ICommand, TestCommand>(TestCommand.Name);
        services.AddSingleton<CommandProcessor>();
        return services;
    }
}
