namespace ItemCatalogue.Spa.Angular.Server.Options;


public record Authentication(string Name, string Value);

public record CatalogueIdOption(Guid Value);
public record ItemIdOption(Guid[] Values);
public record CategoryIdOption(Guid Value);

// public record ItemCatalogueApiOptions(string BaseUrl, Authentication Authentication, CatalogueIdOption CatalogueIdOption, ItemIdOption ItemIdOption, CategoryIdOption CategoryIdOption);

public record ItemCatalogueApiOptions
{
    public string BaseUrl { get; init; }
    public Authentication Authentication { get; init; }
    public CatalogueIdOption CatalogueIdOption { get; init; }
    public ItemIdOption ItemIdOption { get; init; }
    public CategoryIdOption CategoryIdOption { get; init; }

    public ItemCatalogueApiOptions() { }

    public ItemCatalogueApiOptions(string baseUrl, Authentication authentication, CatalogueIdOption catalogueIdOption, ItemIdOption itemIdOption, CategoryIdOption categoryIdOption)
    {
        BaseUrl = baseUrl;
        Authentication = authentication;
        CatalogueIdOption = catalogueIdOption;
        ItemIdOption = itemIdOption;
        CategoryIdOption = categoryIdOption;
    }
}