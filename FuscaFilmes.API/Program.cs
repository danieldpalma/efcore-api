using System.Text.Json.Serialization;
using FuscaFilmes.API.DbContexts;
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

app.MapGet("/directors", (Context context) =>
    {
        return context.Directors
            .Include(director => director.Movies)
            .ToList();
    })
    .WithOpenApi();

app.MapGet("/directors/{id:int}", (Context context, int id) =>
    {
        return context.Directors
            .Where(director => director.Id == id)
            .Include(director => director.Movies)
            .ToList();
    })
    .WithOpenApi();

app.MapPost("/director", (Context context, Director director) =>
    {
        context.Directors.Add(director);
        context.SaveChanges();
    })
    .WithOpenApi();

app.MapPut("/director/{directorId:int}", (Context context, int directorId, Director directorNew) =>
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
    })
    .WithOpenApi();

app.MapDelete("/director/{directorId:int}", (Context context, int directorId) =>
    {
        var director = context.Directors.Find(directorId);
        if(director != null)
        {
            context.Remove(director);
        }

        context.SaveChanges();
    })
    .WithOpenApi();

app.MapGet("/movie", (Context context) =>
    {
        return context.Movies
            .Include(movie => movie.Director)
            .ToList();
    })
    .WithOpenApi();

app.MapGet("/movie/{id:int}", (Context context, int id) =>
    {
      return context.Movies
          .Where(movie => movie.Id == id)
          .Include(movie => movie.Director)
          .ToList();
    })
    .WithOpenApi();

app.MapGet("/movie/byName/{title}", (Context context, string title) =>
    {
        return context.Movies
            .Where(movie => movie.Title.Contains(title))
            .Include(movie => movie.Director)
            .ToList();
    })
    .WithOpenApi();

app.Run();
