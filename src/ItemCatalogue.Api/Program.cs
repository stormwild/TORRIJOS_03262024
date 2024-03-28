using System.Diagnostics;
using System.Security.Cryptography;

using ItemCatalogue.Api.Modules.ApiKeyModule;
using ItemCatalogue.Api.Modules.SwaggerModule;
using ItemCatalogue.Infrastructure;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();


builder.Services.AddDbContext<CatalogueDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddHealthChecks()
                .AddDbContextCheck<CatalogueDbContext>();

builder.Services.AddApiKeyAuthentication(builder.Configuration);
builder.Services.AddApiKeyAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<CatalogueDbContext>();
    context.Database.EnsureCreated(); // comment out when running dotnet ef database update 
    // context.Database.Migrate(); // needed when running dotnet ef database update
    Seeder.Initialize(context);
};

app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyAuthorization>();

app.MapHealthChecks("/health");

app.MapGet("/", async (ILogger<Program> _logger, CatalogueDbContext ctx) =>
{
    var start = Stopwatch.StartNew();
    // _logger.LogInformation("Before GET /");

    var catalogue = await ctx.Catalogues.SingleAsync();

    // _logger.LogInformation("After GET / {ElapsedMilliseconds}", start.ElapsedMilliseconds);
    start.Stop();

    return Results.Ok($"Hello, world! {catalogue.Name}");
})
.WithGroupName("HelloWorld")
.WithDisplayName("Hello World")
.WithName("HelloWorld")
.Produces<string>(StatusCodes.Status200OK, "text/plain")
.Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
.RequireApiKey();

app.Run();