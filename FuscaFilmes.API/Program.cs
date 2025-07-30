using System.Text.Json.Serialization;
using FuscaFilmes.API.DbContexts;
using FuscaFilmes.API.EndpointHandlers;
using FuscaFilmes.API.Entities;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("FuscaFilmesDb"))
        .LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.AllowTrailingCommas = true;
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/directors", DirectorHandlers.GetDirectors)
    .WithOpenApi();

app.MapGet("/directors/{id:int}", DirectorHandlers.GetDirectorById)
    .WithOpenApi();

app.MapGet("/directors/{name}", DirectorHandlers.GetDirectorByName)
    .WithOpenApi();

app.MapPost("/directors", DirectorHandlers.CreateDirector)
    .WithOpenApi();

app.MapPut("/director/{directorId:int}", DirectorHandlers.UpdateDirector)
    .WithOpenApi();

app.MapDelete("/director/{directorId:int}", DirectorHandlers.DeleteDirector)
    .WithOpenApi();


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

app.Run();
