using FuscaFilmes.API.DbContexts;
using FuscaFilmes.API.Entities;

var builder = WebApplication.CreateBuilder(args);

using (var context = new Context())
{
    context.Database.EnsureCreated();
};

builder.Services.AddSwaggerGen();

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
    return context.Directors.ToList();
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

app.MapDelete("/director", (int directorId) =>
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
