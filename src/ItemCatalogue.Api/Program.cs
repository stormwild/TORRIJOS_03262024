using System.Text.Json;

using FastEndpoints;
using FastEndpoints.Swagger;

using ItemCatalogue.Api;
using ItemCatalogue.Api.Modules.ApiKeyModule;
using ItemCatalogue.Api.Modules.SwaggerModule;
using ItemCatalogue.Infrastructure;

using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerConfiguration();


builder.Services.AddDbContext<CatalogueDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddHealthChecks()
                .AddDbContextCheck<CatalogueDbContext>();

builder.Services.AddApiKeyAuthentication(builder.Configuration);
builder.Services.AddApiKeyAuthorization();

builder.Services.AddFastEndpoints()
                .AddAuthorization()
                .AddAuthentication(ApiKeyConstants.ApiKeySecurity)
                .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthentication>(ApiKeyConstants.ApiKeySecurity, null);

builder.Services.SwaggerDocument(o => o.SetOptions(builder)); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseOpenApi();
//     app.UseSwaggerUi();
// }



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

const string ENDPOINT_PREFIX = "api";

app.UseFastEndpoints(c =>
    {
        c.Endpoints.RoutePrefix = ENDPOINT_PREFIX;
        c.Serializer.Options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        c.Errors.UseProblemDetails();
    })
   .UseSwaggerGen();

app.MapHealthChecks("/health");

// app.MapGet("/", async (ILogger<Program> _logger, CatalogueDbContext ctx) =>
// {
//     var start = Stopwatch.StartNew();
//     // _logger.LogInformation("Before GET /");

//     var catalogue = await ctx.Catalogues.SingleAsync();

//     // _logger.LogInformation("After GET / {ElapsedMilliseconds}", start.ElapsedMilliseconds);
//     start.Stop();

//     return Results.Ok($"Hello, world! {catalogue.Name}");
// })
// .WithOpenApi(operation => new(operation)
// {
//     Summary = "Returns Hello World! and the name of the catalogue",
//     Description = "This endpoint returns a simple string message and the name of the first catalogue from the database."
// })
// .Produces<string>(StatusCodes.Status200OK, "text/plain")
// .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
// .RequireApiKey();

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler();
}

app.Run();