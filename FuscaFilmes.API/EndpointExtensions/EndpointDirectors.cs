using FuscaFilmes.API.EndpointHandlers;

namespace FuscaFilmes.API.EndpointExtensions;

public static class EndpointDirectors
{
	public static void DirectorsEnpoints(this IEndpointRouteBuilder app)
	{
		app.MapGet("/directors", DirectorHandlers.GetDirectors)
			.WithOpenApi();

		app.MapGet("/directors/{id:int}", DirectorHandlers.GetDirectorById)
			.WithOpenApi();

		app.MapGet("/directors/{name}", DirectorHandlers.GetDirectorByName)
			.WithOpenApi();

		app.MapPost("/directors", DirectorHandlers.CreateDirector)
			.WithOpenApi();

		app.MapPut("/director/", DirectorHandlers.UpdateDirector)
			.WithOpenApi();

		app.MapDelete("/director/{directorId:int}", DirectorHandlers.DeleteDirector)
			.WithOpenApi();

	}
}