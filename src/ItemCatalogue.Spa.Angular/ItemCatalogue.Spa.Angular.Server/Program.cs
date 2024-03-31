using System.Diagnostics;

using ItemCatalogue.Spa.Angular.Server.Clients;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var authentication = builder.Configuration.GetSection("ItemCatalogueApi:Authentication").Get<Authentication>();
ArgumentNullException.ThrowIfNull(authentication);

builder.Services.AddHttpClient<IItemCatalogueApiClient, ItemCatalogueApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("ItemCatalogueApi:BaseUrl")!);
    client.DefaultRequestHeaders.Add(authentication.Name, authentication.Value);
});

builder.Services.Configure<CatalogueId>(builder.Configuration.GetSection("ItemCatalogueApi:CatalogueId"));

builder.Services.AddProblemDetails();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument();



var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", async (IItemCatalogueApiClient client, IOptions<CatalogueId> options) =>
{
    var response = await client.GetCatalogueItemsAsync(options.Value.Value);

    Debug.WriteLine($"CatalogueName: {response.Name}");

    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.MapFallbackToFile("/index.html");

app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}