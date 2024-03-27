# Use Microsoft's official build .NET image.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the Docker container
WORKDIR /app

EXPOSE 80
EXPOSE 443

ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet tool install --global dotnet-ef --version 8.0.3

# Copy all solutions files to the working directory
COPY . ./

RUN dotnet restore

RUN dotnet ef database update --project src/ItemCatalogue.Api/

RUN dotnet publish src/ItemCatalogue.Api -c Release -o out

# Use Microsoft's official runtime .NET image.
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/out .

EXPOSE 5000

ENTRYPOINT ["dotnet", "ItemCatalogue.Api.dll"]
