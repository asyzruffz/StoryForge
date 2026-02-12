namespace StoryForge.Core.Data;

public class AppInfo
{
    public string Name { get; set; } = "Story Forge";
    public string Version { get; set; } = "0.0.0";
    public string Description { get; set; } = string.Empty;
    public string Runtime { get; set; } = string.Empty;
    public string Platform { get; set; } = string.Empty;
    public string[] Technologies { get; set; } = [];
    public string SourceLink { get; set; } = string.Empty;

    public static AppInfo Default => new()
    {
        Name = "Story Forge",
        Version = "1.0.0",
        Description = "AI-powered story writing assistant",
        Runtime = $".NET {Environment.Version}",
        Platform = Environment.OSVersion.ToString(),
        Technologies = ["Blazor", "Photino", "MudBlazor", "MediatR"],
        SourceLink = "https://github.com/asyzruffz/StoryForge",
    };
}
