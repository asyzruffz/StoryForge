namespace StoryForge.Core.Projects;

public class Project
{
    public string FilePath { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public bool IsFavourite { get; set; } = false;

    public DateTime LastActiveLocal => LastActive.ToLocalTime();

    public void SetActive()
    {
        LastActive = DateTime.UtcNow;
    }
}
