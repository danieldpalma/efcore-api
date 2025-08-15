using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contracts;

namespace FuscaFilmes.API.EndpointHandlers;

public static class DirectorHandlers
{
	public static async Task<IResult> GetDirectorsAsync(IDirectorRepository directorRepository)
	{
		var directors = (await directorRepository.GetDirectorsAsync()).ToList();

		return directors.Count == 0 ? Results.NoContent() : Results.Ok(directors);
	}

	public static async Task<IResult> GetDirectorByIdAsync(IDirectorRepository directorRepository, int id)
	{
		var director = await directorRepository.GetDirectorByIdAsync(id);

		return director.Id == 0 ? Results.NotFound("Director not found.") : Results.Ok(director);
	} 
	
	public static async Task<IResult> GetDirectorByNameAsync(string name, IDirectorRepository directorRepository)
	{
		var director = await directorRepository.GetDirectorByNameAsync(name);

		return director.Id == 0 ? Results.NotFound("Director not found.") : Results.Ok(director);
	} 
	
	public static async Task<IResult> CreateDirectorAsync(IDirectorRepository directorRepository, Director director)
	{
		await directorRepository.AddAsync(director);
		await directorRepository.SaveChangesAsync();
		
		return Results.Created("Director created.", director);
	}

	public static async Task<IResult> UpdateDirectorAsync(IDirectorRepository directorRepository, Director directorNew)
	{
		await directorRepository.UpdateAsync(directorNew);
		await directorRepository.SaveChangesAsync();
		
		return Results.Ok("Director updated.");
	}

	public static async Task<IResult> DeleteDirectorAsync(IDirectorRepository directorRepository, int directorId)
	{
		await directorRepository.DeleteAsync(directorId);
		await directorRepository.SaveChangesAsync();

		return Results.Ok("Director deleted.");
	}
}