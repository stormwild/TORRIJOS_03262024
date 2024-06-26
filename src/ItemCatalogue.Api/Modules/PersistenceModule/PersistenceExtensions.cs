﻿using System.Diagnostics;

using ItemCatalogue.Core.Repositories;
using ItemCatalogue.Infrastructure;
using ItemCatalogue.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ItemCatalogue.Api.Modules.PersistenceModule;

public static class PersistenceExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICatalogueRepository, CatalogueRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IItemRepository, ItemRepository>();
    }

    public static async Task UseSeeder(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<CatalogueDbContext>();

            // var connection = context.Database.GetDbConnection();
            // Debug.WriteLine($"Connection string: {connection.ConnectionString}");

            context.Database.EnsureCreated(); // needed when running dotnet ef database update
            // context.Database.Migrate(); // needed when running dotnet ef database update

            await Seeder.Initialize(context);
        };
    }
}
