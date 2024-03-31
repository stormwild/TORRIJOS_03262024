using ItemCatalogue.Infrastructure;

using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace ItemCatalogue.Tests;

public class ItemCatalogueApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public ItemCatalogueApiTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    // Failing tests due to sqlite in memory not supporting migrations
    // [Fact]
    public async Task GetCatalogueItems_Returns_Catalogue_With_Empty_Items()
    {
        // Arrange
        // using (var scope = _factory.Services.CreateScope())
        // {
        //     var scopedServices = scope.ServiceProvider;
        //     var db = scopedServices.GetRequiredService<CatalogueDbContext>();
        //     db.Database.EnsureCreated();
        //     Seeder.Initialize(db);
        // }

        var catalogueId = "88BD0000-F588-04D9-3C54-08DC4E40D836";

        // Act
        var response = await _client.GetAsync($"catalogue/{catalogueId}/items");

        // Assert
        response.EnsureSuccessStatusCode(); // Status Code 200-299
        Assert.Equal("application/json; charset=utf-8",
            response.Content.Headers.ContentType.ToString());
    }

    //  [Theory]
    // [InlineData("/")]
    // [InlineData("/Index")]
    // [InlineData("/About")]
    // [InlineData("/Privacy")]
    // [InlineData("/Contact")]
    // public async Task Get_EndpointsReturnSuccessAndCorrectContentType(string url)
    // {
    //     // Arrange
    //     var client = _factory.CreateClient();

    //     // Act
    //     var response = await client.GetAsync(url);

    //     // Assert
    //     response.EnsureSuccessStatusCode(); // Status Code 200-299
    //     Assert.Equal("text/html; charset=utf-8", 
    //         response.Content.Headers.ContentType.ToString());
    // }
}
