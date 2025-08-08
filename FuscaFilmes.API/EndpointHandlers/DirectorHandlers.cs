using FuscaFilmes.Domain.Entities;
using FuscaFilmes.Repo.Contracts;

namespace FuscaFilmes.API.EndpointHandlers;

public static class DirectorHandlers
{
	public static IResult GetDirectors(IDirectorRepository directorRepository)
	{
		var directors = directorRepository.GetDirectors().ToList();

		return directors.Count <= 0 ? Results.NoContent() : Results.Ok(directors);
	}

	public static IResult GetDirectorById(IDirectorRepository directorRepository, int id)
	{
		var director = directorRepository.GetDirectorById(id);

		return director.Id == 0 ? Results.NotFound("Director not found.") : Results.Ok(director);
	} 
	
	public static IResult GetDirectorByName(string name, IDirectorRepository directorRepository)
	{
		var director = directorRepository.GetDirectorByName(name);

		return director.Id == 0 ? Results.NotFound("Director not found.") : Results.Ok(director);
	} 
	
	public static IResult CreateDirector(IDirectorRepository directorRepository, Director director)
	{
		directorRepository.Add(director);
		directorRepository.SaveChanges();
		
		return Results.Created("Director created.", director);
	}

	public static IResult UpdateDirector(IDirectorRepository directorRepository, Director directorNew)
	{
		directorRepository.Update(directorNew);
		directorRepository.SaveChanges();
		
		return Results.Ok("Director updated.");
	}

	public static IResult DeleteDirector(IDirectorRepository directorRepository, int directorId)
	{
		directorRepository.Delete(directorId);
		directorRepository.SaveChanges();

		return Results.Ok("Director deleted.");
	}
}