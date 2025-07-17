using FuscaFilmes.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.DbContexts;

public class Context : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Director> Directors { get; set; }

    public Context(DbContextOptions options ) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Director>()
            .HasMany(e => e.Movies)
            .WithOne(e => e.Director)
            .HasForeignKey(e => e.DirectorId)
            .IsRequired();
    }
}