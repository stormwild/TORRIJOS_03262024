using System.Data.Common;

using ItemCatalogue.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ItemCatalogue.Tests;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the app's CatalogueDbContext registration.
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                    typeof(DbContextOptions<CatalogueDbContext>));

            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        });

        builder.ConfigureTestServices(services =>
        {
            // Add CatalogueDbContext using an in-memory database for testing.
            services.AddDbContext<CatalogueDbContext>(options =>
            {
                options.UseSqlite("DataSource=:memory:");
            });
        });

        builder.UseEnvironment("Development");
    }
}
