using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Data;
using StoryForge.Infrastructure.Database.SQLite.Configurations;

namespace StoryForge.Infrastructure.Database.SQLite;

public class ProjectDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Summary> Summaries { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Plot> Plots { get; set; }
    public DbSet<StorySetting> StorySettings { get; set; }
    public DbSet<Chapter> Chapters { get; set; }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new AuthorConfiguration());
        modelBuilder.ApplyConfiguration(new SummaryConfiguration());
        modelBuilder.ApplyConfiguration(new CharacterConfiguration());
        modelBuilder.ApplyConfiguration(new PlotConfiguration());
        modelBuilder.ApplyConfiguration(new StorySettingConfiguration());
        modelBuilder.ApplyConfiguration(new ChapterConfiguration());
    }
}
