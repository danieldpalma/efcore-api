using FuscaFilmes.API.DbContexts;
using FuscaFilmes.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.EndpointHandlers;

public static class DirectorHandlers
{
	public static IEnumerable<Director> GetDirectors(Context context)
	{
		return context.Directors
			.Include(director => director.Movies)
			.OrderBy(director => director.Name)
			.ToList();
	}

	public static Director GetDirectorById(Context context, int id)
	{
		var director = context.Directors
			.Where(director => director.Id == id)
			.Include(director => director.Movies)
			.FirstOrDefault() ?? new Director {Id = 0, Name = "Director not found."};
		
		return director;
	} 
	
	public static Director GetDirectorByName(string name, Context context)
	{
		var director = context.Directors
			.Include(director => director.Movies)
			.FirstOrDefault(director => director.Name.Contains(name)) ?? new Director {Id = 0, Name = "Director not found."};
		
		return director;
	} 
	
	public static Director CreateDirector(Context context, Director director)
	{
		context.Directors.Add(director);
		context.SaveChanges();
		
		return director;
	}

	public static void UpdateDirector(Context context, int directorId, Director directorNew)
	{
		var director = context.Directors.Find(directorId);
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

		context.SaveChanges();
	}

	public static IResult DeleteDirector(Context context, int directorId)
	{
		var director = context.Directors.Find(directorId);
		
		if(director == null)
		{
			return Results.NotFound();
		}
		context.Directors.Remove(director);
		context.SaveChanges();

		return Results.Ok("Director deleted.");
	}
}