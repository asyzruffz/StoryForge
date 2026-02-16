using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using StoryForge.Core.Data;
using StoryForge.Infrastructure.Database.SQLite.Configurations;
using System.Data.Common;

namespace StoryForge.Infrastructure.Database.SQLite;

public class ApplicationDbContext : DbContext
{
    public DbSet<Project> Projects { get; set; }

    DbConnection? connection;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        connection = new SqliteConnection("DataSource=./projects.db");
        connection.Open();

        optionsBuilder.UseSqlite(connection);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProjectConfiguration());
    }

    public override void Dispose()
    {
        connection?.Dispose();
        base.Dispose();
    }
}
