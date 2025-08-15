using FuscaFilmes.API.EndpointHandlers;

namespace FuscaFilmes.API.EndpointExtensions;

public static class EndpointDirectors
{
	public static void DirectorsEndpoints(this IEndpointRouteBuilder app)
	{
		app.MapGet("/directors", DirectorHandlers.GetDirectorsAsync)
			.WithOpenApi();

		app.MapGet("/directors/{id:int}", DirectorHandlers.GetDirectorByIdAsync)
			.WithOpenApi();

		app.MapGet("/directors/{name}", DirectorHandlers.GetDirectorByNameAsync)
			.WithOpenApi();

		app.MapPost("/directors", DirectorHandlers.CreateDirectorAsync)
			.WithOpenApi();

		app.MapPut("/director/", DirectorHandlers.UpdateDirectorAsync)
			.WithOpenApi();

		app.MapDelete("/director/{directorId:int}", DirectorHandlers.DeleteDirectorAsync)
			.WithOpenApi();

	}
}