using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Data;

namespace StoryForge.Infrastructure.Database.SQLite;

public class ProjectDbContext : DbContext
{
    public DbSet<Summary> Summaries { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Plot> Plots { get; set; }
    public DbSet<StorySetting> StorySettings { get; set; }
    public DbSet<Chapter> Chapters { get; set; }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder.ApplyConfiguration(new SummaryConfiguration());

        Database.EnsureCreated();
    }
}
