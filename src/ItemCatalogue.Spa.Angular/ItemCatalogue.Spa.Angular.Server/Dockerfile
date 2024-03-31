#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS with-node
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash
RUN apt-get -y install nodejs
RUN npm install -g pnpm
RUN pnpm setup
RUN pnpm install -g @angular/cli

FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ItemCatalogue.Spa.Angular.Server/ItemCatalogue.Spa.Angular.Server.csproj", "ItemCatalogue.Spa.Angular.Server/"]
COPY ["itemcatalogue.spa.angular.client/itemcatalogue.spa.angular.client.esproj", "itemcatalogue.spa.angular.client/"]
RUN dotnet restore "./ItemCatalogue.Spa.Angular.Server/ItemCatalogue.Spa.Angular.Server.csproj"
COPY . .
WORKDIR "/src/ItemCatalogue.Spa.Angular.Server"
RUN dotnet build "./ItemCatalogue.Spa.Angular.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ItemCatalogue.Spa.Angular.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ItemCatalogue.Spa.Angular.Server.dll"]
