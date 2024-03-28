# docker run -p 5000:8080 -d itemcatalogueapi:1.0
docker run -p 8080:5000 -e ASPNETCORE_URLS=http://+:5000 -d itemcatalogueapi:1.0
