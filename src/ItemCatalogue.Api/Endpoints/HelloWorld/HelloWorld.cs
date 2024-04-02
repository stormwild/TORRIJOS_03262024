using ItemCatalogue.Infrastructure;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace ItemCatalogue.Api;

public static class HelloWorld
{
    public static void MapHelloWorld(this WebApplication app)
    {
        app.MapGet("/hello", HandleAsync)
        .WithOpenApi(o =>
        {
            o.Tags = [new OpenApiTag { Name = "Hello World" }];
            o.Summary = "Hello World";
            o.Description = "A simple hello world endpoint";

            return o;
        })
        .Produces<string>(StatusCodes.Status200OK, "text/plain")
        .RequireAuthorization();
    }

    public static async Task<Results<Ok<string>, NotFound>> HandleAsync(
        ILogger<string> logger, CatalogueDbContext db, HttpContext ctx, CancellationToken ct)
    {
        var catalogue = await db.Catalogues.FirstOrDefaultAsync(ct);

        if (catalogue is null)
        {
            return TypedResults.NotFound();
        }

        logger.LogInformation("Hello, world! {Name}", catalogue.Name);

        return TypedResults.Ok($"Hello, world! {catalogue.Name} {ctx.User.Identity?.IsAuthenticated ?? false} {ctx.User.Identity?.Name}");
    }
}
