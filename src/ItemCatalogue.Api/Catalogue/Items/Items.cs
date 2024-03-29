using FastEndpoints;

using FluentValidation.Results;

using ItemCatalogue.Api.Modules.ApiKeyModule;
using ItemCatalogue.Core.Models;
using ItemCatalogue.Infrastructure;

using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ItemCatalogue.Api;

public class Items : Endpoint<PaginatedItemsRequest, Results<Ok<PaginatedItemsResponse>, BadRequest, NotFound>>
{
    private readonly CatalogueDbContext _context;

    public Items(CatalogueDbContext context)
    {
        _context = context;
    }

    public override void Configure()
    {
        Get("/catalogue/{CatalogueId}/items");
        Tags("Catalog Items"); // Tagging as per OpenAPI spec
        Description(b => b
            .ProducesProblemFE<ProblemDetails>(StatusCodes.Status400BadRequest)
            .ProducesProblemFE<ProblemDetails>(StatusCodes.Status404NotFound)
            .ProducesProblemFE<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .RequireApiKey()
        );
        Summary(s =>
        {
            s.Summary = "Returns Items in the given Catalogue";
            s.Description = "Returns a paginated result of Items in the given Catalogue";
        });
    }

    public override async Task<Results<Ok<PaginatedItemsResponse>, BadRequest, NotFound>> HandleAsync(PaginatedItemsRequest request, CancellationToken ct)
    {
        var catalogue = await _context.Catalogues.Select(c => new
        {
            c.Id,
            c.Name,
            Items = c.Items
                .OrderBy(i => i.Name)
                .Skip(request.Offset)
                .Take(request.Limit)
                .Select(i => new Item(i.Id, i.Name, i.PrimaryCategory.Name))
        })
        .FirstOrDefaultAsync(c => c.Id == new CatalogueId(request.CatalogueId), ct);

        if (catalogue is null)
        {
            return TypedResults.NotFound();
        }

        var response = new PaginatedItemsResponse
        {
            Items = catalogue.Items.ToList().AsReadOnly(),
            TotalItems = catalogue.Items.Count()
        };

        return TypedResults.Ok(response);
    }
}
