using FuscaFilmes.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.DbContexts;

public class Context : DbContext
{
    DbSet<Movie> Movies { get; set; }
    DbSet<Director> Directors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=FuscaFilmes.db");
    }
}