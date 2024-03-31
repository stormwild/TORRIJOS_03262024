using ItemCatalogue.Api.Client;

var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("X-Api-Key", "_ic_9fad2b4be649887c70a58b869c8838075b0dcf91554e64e2b367ba3079d079f5_fea");

var client = new Client(httpClient);
var result = await client.GetCatalogueItemsAsync(Guid.Parse("88bd0000-f588-04d9-3c54-08dc4e40d836"));

var hello = await client.GetAsync();

Console.WriteLine($"Result: {result}");
Console.WriteLine($"Result: {hello}");
Console.ReadKey();
