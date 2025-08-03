using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contracts;
using FuscaFilmes.Repo.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.Repo.Repository;

public class DirectorRepository(Context context) : IDirectorRepository
{
	public IEnumerable<Director> GetDirectors()
	{
		return context.Directors
			.Include(director => director.Movies)
			.OrderBy(director => director.Name)
			.ToList();
	}

	public Director GetDirectorById(int id)
	{
		var director = context.Directors
			.Where(director => director.Id == id)
			.Include(director => director.Movies)
			.FirstOrDefault() ?? new Director {Id = 0, Name = "Director not found."};
		
		return director;
	}

	public Director GetDirectorByName(string name)
	{
		var director = context.Directors
			.Include(director => director.Movies)
			.FirstOrDefault(director => director.Name.Contains(name)) ?? new Director {Id = 0, Name = "Director not found."};
		
		return director;
	}

	public void Add(Director director)
	{
		context.Directors.Add(director);
	}

	public void Update(Director directorNew)
	{
		var director = context.Directors.Find(directorNew.Id);
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

	public void Delete(int directorId)
	{
		var director = context.Directors.Find(directorId);

		if(director != null)
		{
			context.Directors.Remove(director);
		}
	}

	public bool SaveChanges()
	{
		return context.SaveChanges() > 0;
	}
}