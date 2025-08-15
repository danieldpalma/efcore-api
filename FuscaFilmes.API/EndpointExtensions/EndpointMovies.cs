using FuscaFilmes.API.EndpointHandlers;

namespace FuscaFilmes.API.EndpointExtensions;

public static class EndpointMovies
{
	public static void MoviesEndpoints(this IEndpointRouteBuilder app)
	{
		app.MapGet("/movie", MovieHandlers.GetMoviesAsync)
			.WithOpenApi();

		app.MapGet("/movies/{id:int}", MovieHandlers.GetMovieByIdAsync)
			.WithOpenApi();

		app.MapGet("/movie/byName/{title}", MovieHandlers.GetMovieByTitleAsync)
			.WithOpenApi();

		app.MapPatch("/movies/", MovieHandlers.UpdateMovieAsync)
			.WithOpenApi();

		app.MapDelete("/movies/{id:int}", MovieHandlers.DeleteMovieAsync)
			.WithOpenApi();
	}
}