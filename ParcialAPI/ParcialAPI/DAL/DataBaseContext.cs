using Microsoft.EntityFrameworkCore;
using ParcialAPI.DAL.Entities;

namespace ParcialAPI.DAL;

public class DataBaseContext : DbContext    
{
    public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Country>()
            .HasIndex(c => c.Name).IsUnique();
        modelBuilder.Entity<State>().HasIndex("Name", "CountryId").IsUnique();
    }

    #region DbSets
    public DbSet<Country> Countries { get; set; }
    public DbSet<State> States { get; set; }

    #endregion
}
