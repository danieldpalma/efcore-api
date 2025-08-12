namespace FuscaFilmes.Domain.Entities;

public class Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int ReleasedYear { get; set; }
    public ICollection<DirectorMovie> DirectorsMovies { get; set; } = null!;
    public ICollection<Director> Directors { get; set; } = null!;
}