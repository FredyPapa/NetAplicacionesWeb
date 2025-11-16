using ECommerceWeb.WebApi.DataAccess;
using ECommerceWeb.WebApi.Repositories.Interfaces;
using ECommerceWeb.WebApi.Repositories.Services;
using ECommerceWeb.WebApi.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddDbContext<ECommerceDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ECommerceDB"));
});

// Registro en el DI Container la dependencia del ICategoriaRepository
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();
builder.Services.AddScoped<IProductoRepository, ProductoRepository>();
builder.Services.AddScoped<IFileUploader, FileUploader>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.MapFallbackToFile("index.html");

app.MapControllers();

//Minimal API
app.MapGet("/api/marcas",async(IMarcaRepository marcaRepository) =>
{
    var marcas = await marcaRepository.ListAsync();
    return Results.Ok(marcas);
});

app.Run();

