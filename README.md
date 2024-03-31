# ItemCatalogue

## Setup


When Testing in Swagger or .http use the following:

`X-Api-Key` value `_ic_9fad2b4be649887c70a58b869c8838075b0dcf91554e64e2b367ba3079d079f5_fea`

`catalogueId` value `88bd0000-f588-04d9-3c54-08dc4e40d836`

## Add Migration

```bash
cd ItemCatalogue.Infrastructure

dotnet ef migrations add InitialCreate -s ../ItemCatalogue.Api/ --context CatalogueDbContext

dotnet ef database update -s ../ItemCatalogue.Api/ItemCatalogue.Api.csproj --context CatalogueDbContext
```
