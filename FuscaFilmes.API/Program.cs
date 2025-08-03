using System.Text.Json.Serialization;
using FuscaFilmes.Repo.DbContexts;
using FuscaFilmes.API.EndpointExtensions;
using FuscaFilmes.Repo.Contracts;
using FuscaFilmes.Repo.Repository;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("FuscaFilmesDb"))
        .LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddScoped<IDirectorRepository, DirectorRepository>();

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

app.DirectorsEnpoints();
app.MoviesEnpoints();

app.Run();
