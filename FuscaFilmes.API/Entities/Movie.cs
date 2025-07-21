namespace FuscaFilmes.API.Entities;

public class Movie
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public int ReleasedYear { get; set; }
    public required int DirectorId { get; set; }
    public Director Director { get; set; } = null!;
}