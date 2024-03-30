using ItemCatalogue.Infrastructure;

namespace ItemCatalogue.Api.Modules.PersistenceModule;

public static class PersistenceExtensions
{
    public static void UseSeeder(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<CatalogueDbContext>();
            context.Database.EnsureCreated(); // comment out when running dotnet ef database update 
            // context.Database.Migrate(); // needed when running dotnet ef database update
            Seeder.Initialize(context);
        };
    }
}
