using FuscaFilmes.Repo.DbContexts;
using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.EndpointHandlers;

public static class MovieHandlers
{
	public static IEnumerable<Movie> GetMovies(Context context)
	{
		return context.Movies
			.Include(movie => movie.Directors)
			.OrderBy(movie => movie.Title)
			.ToList();
	}

	public static IResult GetMovieById(Context context, int id)
	{
		var movie = context.Movies
			.Where(movie => movie.Id == id)
			.Include(movie => movie.Directors)
			.FirstOrDefault();

		return movie != null ? Results.Ok(movie) : Results.NotFound();
	}

	public static IResult GetMovieByTitle(Context context, string title)
	{
		var movie = context.Movies
			.Where(movie => movie.Title.Contains(title))
			.Include(movie => movie.Directors)
			.ToList();

		return movie.Count != 0 ? Results.Ok(movie) : Results.NotFound();
	}

	public static IResult UpdateMovie(Context context, MovieUpdate movieNew)
	{
		var movie = context.Movies.Find(movieNew.Id);
		
		if(movie == null)
		{
			return Results.NotFound("Movie not found.");
		}
		
		movie.Title = movieNew.Title;
		movie.ReleasedYear = movieNew.ReleasedYear;
		
		context.Movies.Update(movie);
		context.SaveChanges();
		
		return Results.Ok("Movie updated.");
	}

	public static IResult DeleteMovie(Context context, int id)
	{
		var movie = context.Movies.Find(id);

		if(movie == null)
		{
			return Results.NotFound("Movie not found.");
		}
		
		context.Movies.Remove(movie);
		context.SaveChanges();
		
		return Results.Ok("Movie deleted.");
	}
}