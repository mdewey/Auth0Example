using System;
using System.Text.RegularExpressions;
using Auth0Example.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace content
{
  public partial class DatabaseContext : DbContext
  {
    public DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    private string ConvertPostConnectionToConnectionString(string connection)
    {
      var _connection = connection.Replace("postgres://", String.Empty);
      var output = Regex.Split(_connection, ":|@|/");
      return $"server={output[2]};database={output[4]};User Id={output[0]}; password={output[1]}; port={output[3]}";
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        var envConn = Environment.GetEnvironmentVariable("DATABASE_URL");
        var conn = "server=localhost;database=Monsters";
        if (envConn != null)
        {
          conn = ConvertPostConnectionToConnectionString(envConn);
        }
        optionsBuilder.UseNpgsql(conn);
      }
    }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");
      modelBuilder.Entity<Monsters>().HasData(
          new Monsters { Id = -1, Name = "Ghoul", CR = 5, TotalHealth = 26 },
          new Monsters { Id = -2, Name = "Zombie", CR = 1, TotalHealth = 6 },
          new Monsters { Id = -3, Name = "Carrion Crow", CR = 5, TotalHealth = 30 },
          new Monsters { Id = -4, Name = "Chimera", CR = 8, TotalHealth = 125 }
      );
    }

    public DbSet<Monsters> Monsters { get; set; }
  }
}
