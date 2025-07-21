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

        modelBuilder.Entity<Director>().HasData(
            new Director { Id = 1, Name = "Christopher Nolan" },
            new Director { Id = 2, Name = "Steven Spielberg" },
            new Director { Id = 3, Name = "Quentin Tarantino" },
            new Director { Id = 4, Name = "Martin Scorsese" },
            new Director { Id = 5, Name = "Greta Gerwig" }
        );

        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "Inception", ReleasedYear = 2010, DirectorId = 1 },
            new Movie { Id = 2, Title = "Interstellar", ReleasedYear = 2014, DirectorId = 1 },
            new Movie { Id = 3, Title = "Jurassic Park", ReleasedYear = 1993, DirectorId = 2 },
            new Movie { Id = 4, Title = "Schindler's List", ReleasedYear = 1993, DirectorId = 2 },
            new Movie { Id = 5, Title = "Pulp Fiction", ReleasedYear = 1994, DirectorId = 3 },
            new Movie { Id = 6, Title = "Django Unchained", ReleasedYear = 2012, DirectorId = 3 },
            new Movie { Id = 7, Title = "The Wolf of Wall Street", ReleasedYear = 2013, DirectorId = 4 },
            new Movie { Id = 8, Title = "Goodfellas", ReleasedYear = 1990, DirectorId = 4 },
            new Movie { Id = 9, Title = "Lady Bird", ReleasedYear = 2017, DirectorId = 5 },
            new Movie { Id = 10, Title = "Barbie", ReleasedYear = 2023, DirectorId = 5 }
        );
    }
}