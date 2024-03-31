# ItemCatalogue

## Demo Working

Currently only retrieves data from the protected api using an X-Api-Key header and value

The left image shows the Spa BFF Api is able to display data coming from the protected api.

![](/docs/api-calls-api-using-api-key.png)

The angular client then displays the data from bff api

![](/docs/angular-displays-data-from-bff-api.png)

## Pre-requisites

- Git
- Nvm, NodeJs, pnpm
- Visual Studio Code and/or Visual Studio 2022
- .NET 8 Sdk

## Visual Studio

Project Stucture

```bash
$ tree -L 2
.
|-- Directory.Build.props
|-- Directory.Packages.props
|-- ItemCatalogue.Apli.ConsoleClient
|   |-- bin
|   `-- obj
|-- ItemCatalogue.sln
|-- README.md
|-- build.sh
|-- docker-compose.yml
|-- docs
|   |-- Authentication.http
|   |-- angular-displays-data-from-bff-api.png
|   |-- api-calls-api-using-api-key.png
|   |-- curl.sh
|   |-- docs.md
|   `-- snippets.md
|-- nuget.config
|-- run.api.sh
|-- src
|   |-- ItemCatalogue.Api
|   |-- ItemCatalogue.Core
|   |-- ItemCatalogue.Infrastructure
|   `-- ItemCatalogue.Spa.Angular
`-- tests
    `-- ItemCatalogue.Tests

11 directories, 14 files

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
