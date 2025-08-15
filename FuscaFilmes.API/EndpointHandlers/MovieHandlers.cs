using FuscaFilmes.Repo.DbContexts;
using FuscaFilmes.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FuscaFilmes.API.EndpointHandlers;

public static class MovieHandlers
{
	public static async Task<List<Movie>> GetMoviesAsync(Context context)
	{
		return await context.Movies
			.Include(movie => movie.Directors)
			.OrderBy(movie => movie.Title)
			.ToListAsync();
	}

	public static async Task<IResult> GetMovieByIdAsync(Context context, int id)
	{
		var movie = await context.Movies
			.Where(movie => movie.Id == id)
			.Include(movie => movie.Directors)
			.FirstOrDefaultAsync();

		return movie != null ? Results.Ok(movie) : Results.NotFound();
	}

	public static async Task<IResult> GetMovieByTitleAsync(Context context, string title)
	{
		var movie = await context.Movies
			.Include(movie => movie.Directors)
			.Where(movie => movie.Title.Contains(title))
			.ToListAsync();

		return movie.Count != 0 ? Results.Ok(movie) : Results.NotFound();
	}

	public static async Task<IResult> UpdateMovieAsync(Context context, MovieUpdate movieNew)
	{
		var movie = await context.Movies.FindAsync(movieNew.Id);
		
		if(movie == null)
		{
			return Results.NotFound("Movie not found.");
		}
		
		movie.Title = movieNew.Title;
		movie.ReleasedYear = movieNew.ReleasedYear;
		
		context.Movies.Update(movie);
		await context.SaveChangesAsync();
		
		return Results.Ok("Movie updated.");
	}

	public static async Task<IResult> DeleteMovieAsync(Context context, int id)
	{
		var movie = await context.Movies.FindAsync(id);

		if(movie == null)
		{
			return Results.NotFound("Movie not found.");
		}
		
		context.Movies.Remove(movie);
		await context.SaveChangesAsync();
		
		return Results.Ok("Movie deleted.");
	}
}