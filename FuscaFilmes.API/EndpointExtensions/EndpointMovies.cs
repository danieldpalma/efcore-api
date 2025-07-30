using FuscaFilmes.API.EndpointHandlers;

namespace FuscaFilmes.API.EndpointExtensions;

public static class EndpointMovies
{
	public static void MoviesEnpoints(this IEndpointRouteBuilder app)
	{
		app.MapGet("/movie", MovieHandlers.GetMovies)
			.WithOpenApi();

		app.MapGet("/movies/{id:int}", MovieHandlers.GetMovieById)
			.WithOpenApi();

		app.MapGet("/movie/byName/{title}", MovieHandlers.GetMovieByTitle)
			.WithOpenApi();

		app.MapPatch("/movies/", MovieHandlers.UpdateMovie)
			.WithOpenApi();

		app.MapDelete("/movies/{id:int}", MovieHandlers.DeleteMovie)
			.WithOpenApi();
	}
}