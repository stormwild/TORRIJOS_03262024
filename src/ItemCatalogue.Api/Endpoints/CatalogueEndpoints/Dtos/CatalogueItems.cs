namespace ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Dtos;

public record CatalogueItems(Guid Id, string Name, IReadOnlyList<CatalogueItem> Items);
