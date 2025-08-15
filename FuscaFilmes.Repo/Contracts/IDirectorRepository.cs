using FuscaFilmes.Domain.Entities;

namespace FuscaFilmes.Repo.Contracts;

public interface IDirectorRepository
{
	Task<IEnumerable<Director>> GetDirectorsAsync();
	Task<Director> GetDirectorByIdAsync(int id);
	Task<Director> GetDirectorByNameAsync(string name);
	Task AddAsync(Director director);
	Task UpdateAsync(Director director);
	Task DeleteAsync(int directorId);
	Task<bool> SaveChangesAsync();
}