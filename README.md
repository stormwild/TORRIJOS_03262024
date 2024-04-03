# ItemCatalogue

## Demo Working

In a terminal window

```bash
cd src/ItemCatalogue.Api/
dotnet run
```

In another terminal window

```bash
cd ItemCatalogue.Spa.Angular.Server/
dotnet run
```

The `src/ItemCatalogue.Api/` is the api key protected api which also has the SQLite database accessed through EF Core.

The `src/ItemCatalogue.Spa.Angular/ItemCatalogue.Spa.Angular.Server` is the Spa BFF Api which is the client to the protected api.

When run it will also run pnpm start on the angular client.

The angular client makes requests to the bff api which in turn makes requests to the protected api.

[Video Demo](/docs/item-catalogue-api-working.mp4)

![](/docs/itemcatalogueapi-working.png)

The ItemCatalogue.Api is the protected api which is accessed using an X-Api-Key header and value.

In the image below:

The left image shows the Spa BFF Api is able to display data coming from the protected api.

![](/docs/api-calls-api-using-api-key.png)

The angular client then displays the data from bff api

## Pre-requisites

- Git
- Nvm, NodeJs, pnpm
- Visual Studio Code and/or Visual Studio 2022
- .NET 8 Sdk

## Visual Studio or Visual Studio Code

Project Stucture

```bash
 tree -L 3
.
|-- CatalogueDb.session.sql
|-- Directory.Build.props
|-- Directory.Packages.props
|-- ItemCatalogue.sln
|-- README.md
|-- build.sh
|-- docker-compose.yml
|-- docs
|   |-- Authentication.http
|   |-- angular-displays-data-from-bff-api.png
|   |-- api-calls-api-using-api-key.png
|   |-- categories.csv
|   |-- catelogues.csv
|   |-- create-item-response.json
|   |-- create-item.sh
|   |-- curl.sh
|   |-- data.md
|   |-- docs.md
|   |-- item-catalogue-api-working.mp4
|   |-- itemcatalogueapi-working.mp4
|   |-- itemcatalogueapi-working.png
|   |-- items-2.json
|   |-- items.csv
|   |-- snippets.md
|   `-- visual-studio-2022-multiple-start-up-projects.png
|-- nuget.config
|-- run.api.sh
|-- src
|   |-- ItemCatalogue.Api
|   |   |-- Db
|   |   |-- Dockerfile
|   |   |-- Endpoints
|   |   |-- Extensions
|   |   |-- ItemCatalogue.Api.csproj
|   |   |-- ItemCatalogue.Api.csproj.user
|   |   |-- ItemCatalogue.Api.http
|   |   |-- Middleware
|   |   |-- Modules
|   |   |-- Program.cs
|   |   |-- Properties
|   |   |-- appsettings.Development.json
|   |   |-- appsettings.json
|   |   |-- bin
|   |   `-- obj
|   |-- ItemCatalogue.Core
|   |   |-- ItemCatalogue.Core.csproj
|   |   |-- Models
|   |   |-- Repositories
|   |   |-- bin
|   |   `-- obj
|   |-- ItemCatalogue.Infrastructure
|   |   |-- CatalogueDbContext.cs
|   |   |-- Configurations
|   |   |-- ItemCatalogue.Infrastructure.csproj
|   |   |-- Migrations
|   |   |-- Repositories
|   |   |-- Seeder.cs
|   |   |-- bin
|   |   `-- obj
|   `-- ItemCatalogue.Spa.Angular
|       |-- ItemCatalogue.Spa.Angular.Server
|       `-- itemcatalogue.spa.angular.client
`-- tests
    `-- ItemCatalogue.Tests
        |-- CatalogueItemsInMemoryTest.cs
        |-- CustomWebApplicationFactory.cs
        |-- ItemCatalogue.Tests.csproj
        |-- ItemCatalogueApiTests.cs
        |-- MockDb.cs
        |-- Properties
        |-- bin
        `-- obj

30 directories, 42 files

cd src/ItemCatalogue.Spa.Angular/

$ tree -L 1
.
|-- ItemCatalogue.Spa.Angular.Server
`-- itemcatalogue.spa.angular.client

2 directories, 0 files
```

The project is composed of 5 src projects.

The `ItemCatalogue.Api` is dependent on the Core and Infrastructure projects

```bash
|-- src
|   |-- ItemCatalogue.Api
|   |-- ItemCatalogue.Core
|   |-- ItemCatalogue.Infrastructure
```

In Visual Studio, configure the solution to start multiple projects with the Api the first item to start.

![](/docs/visual-studio-2022-multiple-start-up-projects.png)

We may need to run `pnpm i` on the spa client. The spa was modified to use pnpm instead of npm.

```
cd itemcatalogue.spa.angular.client/
pnpm i
```

Only the Api was dockerized with the docker compose running the api.

## Setup

When Testing in Swagger or .http use the following:

`X-Api-Key` value `_ic_9fad2b4be649887c70a58b869c8838075b0dcf91554e64e2b367ba3079d079f5_fea`

`catalogueId` value `88bd0000-f588-04d9-3c54-08dc4e40d836`

## Add Migration

The database has already been created, migrated and initialized with seed data.

When adding additional migrations we can update the SQLite db using the following:

```bash
cd ItemCatalogue.Infrastructure

dotnet ef migrations add InitialCreate -s ../ItemCatalogue.Api/ --context CatalogueDbContext

dotnet ef database update -s ../ItemCatalogue.Api/ItemCatalogue.Api.csproj --context CatalogueDbContext
```
