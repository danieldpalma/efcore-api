namespace FuscaFilmes.Domain.Entities;

public class Director
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<DirectorMovie> DirectorsMovies { get; set; } = null!;
    public ICollection<Movie> Movies { get; set; } = [];
}