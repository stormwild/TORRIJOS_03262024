namespace ItemCatalogue.Spa.Angular.Server.Clients;


public record Authentication(string Name, string Value);

public class CatalogueId
{
    public Guid Value { get; set; }
}
