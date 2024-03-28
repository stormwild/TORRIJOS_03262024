# Use Microsoft's official build .NET image.
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory in the Docker container
WORKDIR /app

ENV PATH="${PATH}:/root/.dotnet/tools"

RUN dotnet tool install --global dotnet-ef --version 8.0.3

# Run the build of this Dockerfile in the context of the solution's root folder

# cd TORRIJOS_03262024
# docker build -f ./src/ItemCatalogue.Api/Dockerfile -t itemcatalogueapi:1.0 .

# or run build.sh, sh ./build.sh 

# When building from docker compose
# docker-compose up --build 

# Copy all solutions files to the working directory
COPY . ./

RUN dotnet restore

RUN dotnet ef database update --project src/ItemCatalogue.Api/

RUN dotnet publish src/ItemCatalogue.Api -c Release -o out

# Use Microsoft's official runtime .NET image.
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "ItemCatalogue.Api.dll"]
