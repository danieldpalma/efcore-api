namespace FuscaFilmes.Domain.Entities;

public class DirectorDetail
{
	public int Id { get; set; }
	public string Bio { get; set; } = string.Empty;
	public DateTime BirthDate { get; set; }
	public int DirectorId { get; set; }
	public Director Director { get; set; } = null!;
}