# ItemCatalogue

## Add Migration

```bash
cd ItemCatalogue.Infrastructure
dotnet ef migrations add InitialCreate -s ../ItemCatalogue.Api/ --context CatalogueDbContext
dotnet ef database update -s ../ItemCatalogue.Api/ItemCatalogue.Api.csproj --context CatalogueDbContext
```
