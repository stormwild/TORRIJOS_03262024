using System.Diagnostics.CodeAnalysis;

using FluentAssertions;

using ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Dtos;
using ItemCatalogue.Api.Endpoints.CatalogueEndpoints.Queries;
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
        var repository = Substitute.For<IItemRepository>();
        _ = repository.GetItemsListAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).ReturnsNullForAnyArgs();

        // Act
        var result = await ItemsList.HandleAsync(repository, Guid.NewGuid(), CancellationToken.None);
        var notFound = result.Result as NotFound;

        // Assert
        result.Should().BeOfType<Results<Ok<CatalogueItems>, NotFound>>();
        notFound.Should().BeOfType<NotFound>();
        notFound.Should().NotBeNull();
    }
}