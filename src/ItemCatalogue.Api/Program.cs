using ItemCatalogue.Infrastructure;

using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddConsole();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApiDocument(options =>
{
    options.DocumentName = "Item Catalogue API v1";
    options.Title = "Item Catalogue API";
    options.Description = "A simple API to manage items in a catalogue";
    options.Version = "1.0";
    options.PostProcess = document =>
    {
        document.Info.Title = "Item Catalogue API";
        document.Info.Description = "A simple API to manage items in a catalogue";
    };
});

builder.Services.AddDbContext<CatalogueDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

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
    context.Database.EnsureCreated();
    // context.Database.Migrate();
    Seeder.Initialize(context);
};

app.UseHttpsRedirection();


app.MapGet("/", async (ILogger<Program> logger, CatalogueDbContext ctx, HttpResponse response) =>
{
    logger.LogInformation("GET /");
    var catalogue = await ctx.Catalogues.SingleAsync();
    await response.WriteAsync($"Hello, world! {catalogue.Name}");
});

app.Run();