
using ItemCatalogue.Api;
using ItemCatalogue.Api.Modules.AuthenticationModule;
using ItemCatalogue.Api.Modules.PersistenceModule;
using ItemCatalogue.Api.Modules.SwaggerModule;
using ItemCatalogue.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration(builder.Configuration);

// Configure Database
builder.Services.AddDbContext<CatalogueDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddRepositories();

builder.Services.AddAuthentication(ApiKeyAuthentication.SchemeName)
    .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>(ApiKeyAuthentication.SchemeName, options =>
    {
        var apiKey = builder.Configuration["Authentication:ApiKey"] ?? throw new InvalidOperationException("ApiKey is not configured");
        options.ApiKey = apiKey;
    });

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler();
}

await app.UseSeeder();

app.UseAuthentication();
app.UseAuthorization();

app.MapHelloWorld();

app.MapItemsList();
app.MapItemGetById();

app.Run();

public partial class Program { }