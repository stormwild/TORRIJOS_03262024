namespace ItemCatalogue.Api.Endpoints.Catalogue.Queries;

public record CatalogueItems(Guid Id, string Name, IReadOnlyList<Item> Items);
