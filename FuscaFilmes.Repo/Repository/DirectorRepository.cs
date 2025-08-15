using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contracts;
using FuscaFilmes.Repo.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.Repo.Repository;

public class DirectorRepository(Context context) : IDirectorRepository
{
	public async Task<IEnumerable<Director>> GetDirectorsAsync()
	{
		return await context.Directors
			.Include(director => director.Movies)
			.OrderBy(director => director.Name)
			.ToListAsync();
	}

	public async Task<Director> GetDirectorByIdAsync(int id)
	{
		var director = await context.Directors
			.Where(director => director.Id == id)
			.Include(director => director.Movies)
			.FirstOrDefaultAsync() ?? new Director {Id = 0, Name = "Director not found."};
		
		return director;
	}

	public async Task<Director> GetDirectorByNameAsync(string name)
	{
		var director = await context.Directors
			.Include(director => director.Movies)
			.FirstOrDefaultAsync(director => director.Name.Contains(name)) ?? new Director {Id = 0, Name = "Director not found."};
		
		return director;
	}

	public async Task AddAsync(Director director)
	{
		await context.Directors.AddAsync(director);
	}

	public async Task UpdateAsync(Director directorNew)
	{
		var director = await context.Directors.FindAsync(directorNew.Id);
		if(director != null)
		{
			director.Name = directorNew.Name;

			if(directorNew.Movies.Count > 0)
			{
				director.Movies.Clear();

				foreach(var movie in directorNew.Movies)
				{
					director.Movies.Add(movie);
				}
			};
		}
	}

	public async Task DeleteAsync(int directorId)
	{
		var director = await context.Directors.FindAsync(directorId);

		if(director != null)
		{
			context.Directors.Remove(director);
		}
	}

	public async Task<bool> SaveChangesAsync()
	{
		return (await context.SaveChangesAsync()) > 0;
	}
}