namespace StoryForge.Core.Data;

public class Project
{
    public string FilePath { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;

    public DateTime LastActiveLocal => LastActive.ToLocalTime();

    public void SetActive()
    {
        LastActive = DateTime.UtcNow;
    }
}
