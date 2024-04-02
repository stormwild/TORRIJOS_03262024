using System.Diagnostics.CodeAnalysis;

using FluentAssertions;

using ItemCatalogue.Api;
using ItemCatalogue.Core.Models;
using ItemCatalogue.Core.Repositories;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using NSubstitute;
using NSubstitute.ReturnsExtensions;

using Xunit.Abstractions;

namespace ItemCatalogue.Tests;

public class GetCatalogueItemsInMemoryTest(ITestOutputHelper output)
{
    private readonly ITestOutputHelper _output = output;

    [Fact]
    public async Task GetCatalogueItems_ReturnsNotFound_WhenNoCatalogueIsFound()
    {
        // Arrange
        var repository = Substitute.For<ICatalogueRepository>();
        _ = repository.GetCatalogueItemsAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNullForAnyArgs();

        // Act
        var result = await CatalogueItemsList.HandleAsync(repository, Guid.NewGuid(), CancellationToken.None);
        var notFound = result.Result as NotFound;

        // Assert
        result.Should().BeOfType<Results<Ok<CatalogueItems>, NotFound>>();
        notFound.Should().BeOfType<NotFound>();
        notFound.Should().NotBeNull();
    }
}