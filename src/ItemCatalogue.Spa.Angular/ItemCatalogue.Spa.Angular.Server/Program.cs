using System.Diagnostics;

using ItemCatalogue.Spa.Angular.Server;
using ItemCatalogue.Spa.Angular.Server.Clients;
using ItemCatalogue.Spa.Angular.Server.Endpoints.CatalogueEndpoints;
using ItemCatalogue.Spa.Angular.Server.Options;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var section = builder.Configuration.GetSection("ItemCatalogueApiOptions");
var options = section.Get<ItemCatalogueApiOptions>();

ArgumentNullException.ThrowIfNull(options);

builder.Services.AddHttpClient<IItemCatalogueApiClient, ItemCatalogueApiClient>(client =>
{
    client.BaseAddress = new Uri(options.BaseUrl);
    client.DefaultRequestHeaders.Add(options.Authentication.Name, options.Authentication.Value);
});

builder.Services.Configure<ItemCatalogueApiOptions>(section);

builder.Services.AddProblemDetails();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:4200", "http://localhost:5165", "*")
                   .AllowAnyHeader()
                   .AllowAnyMethod()
                   .AllowCredentials(); // Allow cookies, HTTP authentication, etc.
        });
});


var app = builder.Build();

app.UseCors();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

app.MapCatalogue();


app.MapFallbackToFile("/index.html");

app.Run();
