using FuscaFilmes.Domain.Entities;

namespace FuscaFilmes.Repo.Contracts;

public interface IDirectorRepository
{
	IEnumerable<Director> GetDirectors();
	Director GetDirectorById(int id);
	Director GetDirectorByName(string name);
	void Add(Director director);
	void Update(Director director);
	void Delete(int directorId);
	
	bool SaveChanges();
}