using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.Repo.DbContexts;

public class Context : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Director> Directors { get; set; }

    public DbSet<DirectorMovie> DirectorsMovies { get; set; }

    public Context(DbContextOptions options ) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Director>()
            .HasMany(e => e.Movies)
            .WithMany(e => e.Directors)
            .UsingEntity<DirectorMovie>(
                dm => dm.HasOne<Movie>(e => e.Movie).WithMany(e => e.DirectorsMovies),
                dm => dm.HasOne<Director>(e => e.Director).WithMany(e => e.DirectorsMovies)
            );

        modelBuilder.Entity<Director>().HasData(
            new Director { Id = 1, Name = "Christopher Nolan" },
            new Director { Id = 2, Name = "Steven Spielberg" },
            new Director { Id = 3, Name = "Quentin Tarantino" },
            new Director { Id = 4, Name = "Martin Scorsese" },
            new Director { Id = 5, Name = "Greta Gerwig" }
        );

        modelBuilder.Entity<Movie>().HasData(
            new Movie { Id = 1, Title = "Inception", ReleasedYear = 2010 },
            new Movie { Id = 2, Title = "Interstellar", ReleasedYear = 2014 },
            new Movie { Id = 3, Title = "Jurassic Park", ReleasedYear = 1993 },
            new Movie { Id = 4, Title = "Schindler's List", ReleasedYear = 1993 },
            new Movie { Id = 5, Title = "Pulp Fiction", ReleasedYear = 1994 },
            new Movie { Id = 6, Title = "Django Unchained", ReleasedYear = 2012 },
            new Movie { Id = 7, Title = "The Wolf of Wall Street", ReleasedYear = 2013 },
            new Movie { Id = 8, Title = "Goodfellas", ReleasedYear = 1990 },
            new Movie { Id = 9, Title = "Lady Bird", ReleasedYear = 2017 },
            new Movie { Id = 10, Title = "Barbie", ReleasedYear = 2023 }
        );
        
        modelBuilder.Entity<DirectorMovie>().HasData(
            new { DirectorId = 1, MovieId = 1 },
            new { DirectorId = 1, MovieId = 2 },
            new { DirectorId = 2, MovieId = 3 },
            new { DirectorId = 2, MovieId = 4 },
            new { DirectorId = 3, MovieId = 5 },
            new { DirectorId = 3, MovieId = 6 },
            new { DirectorId = 4, MovieId = 7 },
            new { DirectorId = 4, MovieId = 8 },
            new { DirectorId = 5, MovieId = 9 },
            new { DirectorId = 5, MovieId = 10 }
        );
    }
}