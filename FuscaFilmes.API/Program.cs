using FuscaFilmes.API.DbContexts;
using FuscaFilmes.API.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

using (var context = new Context())
{
    context.Database.EnsureCreated();
};

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

app.MapGet("/directors", () =>
{
    using var context = new Context();
    return context.Directors
        .Include(director => director.Movies)
        .ToList();
})
.WithOpenApi();

app.MapPost("/director", (Director director) =>
{
    using var context = new Context();
    context.Directors.Add(director);
    context.SaveChanges();
})
.WithOpenApi();

app.MapPut("/director/{directorId}", (int directorId, Director directorNew) =>
{
    using var context = new Context();
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

app.MapDelete("/director/{directorId}", (int directorId) =>
{
    using var context = new Context();
    var director = context.Directors.Find(directorId);
    if(director != null)
    {
        context.Remove(director);
    }

    context.SaveChanges();
})
.WithOpenApi();

app.Run();
