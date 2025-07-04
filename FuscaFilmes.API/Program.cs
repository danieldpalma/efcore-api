using FuscaFilmes.API.DbContexts;

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

app.MapGet("/", () =>
{
    return "Hello World";
})
.WithOpenApi();

app.Run();
