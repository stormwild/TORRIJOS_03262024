# Snippets

```csharp
    var summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

    app.MapGet("/weatherforecast", () =>
    {
        var forecast = Enumerable.Range(1, 5).Select(index =>
            new WeatherForecast
            (
                DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                Random.Shared.Next(-20, 55),
                summaries[Random.Shared.Next(summaries.Length)]
            ))
            .ToArray();
        return forecast;
    })
    .WithName("GetWeatherForecast")
    .WithOpenApi();

```

```csharp
// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("ClientAccess", policy => policy.Requirements.Add(new ClientAccessRequirement()));

// });

// builder.Services.AddSingleton<IAuthorizationHandler, ClientAccessHandler>();

// app.UseAuthorization();

using Microsoft.AspNetCore.Authorization;

namespace ItemCatalogue.Api;

public class ClientAccessHandler : AuthorizationHandler<ClientAccessRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientAccessRequirement requirement)
    {
        context.User.
        context.Succeed(requirement);
        context.Fail(new AuthorizationFailureReason(this, "Missing or invalid client access key or secret"));


        return Task.CompletedTask;
    }
}

using Microsoft.AspNetCore.Authorization;

public class ClientAccessRequirement : IAuthorizationRequirement
{
}

app.Use(async (context, next) =>  {
    
    context.Response.Headers.Add("X-Content-Type-Options", "nosniff");
    context.Response.Headers.Add("X-Frame-Options", "DENY");
    context.Response.Headers.Add("X-XSS-Protection", "1; mode=block");

    await next();
});


app.Use(async (context, next) =>
{
    Debug.WriteLine($"Request received: {DateTime.Now}");

    await next(context);

    Debug.WriteLine($"Request sending: {DateTime.Now}");
});

        // if (!context.Request.Headers.ContainsKey("X-API-KEY"))
        // {
        //     Debug.WriteLine($"Header: {context.Request.Headers["X-API-KEY"]}");
        //     context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        //     await Task.CompletedTask;
        // }
builder.WithMetadata(new RequireApiKeyAtrribute())        


            document.AddSecurity("apikey", Enumerable.Empty<string>(), new OpenApiSecurityScheme
            {
                Type = OpenApiSecuritySchemeType.ApiKey,
                Name = "api_key",
                In = OpenApiSecurityApiKeyLocation.Header
            });
```

```csharp
// Generates an api key
var guid = Guid.NewGuid();
using var sha256 = SHA256.Create();
var hashBytes = sha256.ComputeHash(guid.ToByteArray());
var hexString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
var checksum = hashBytes.Sum(b => b);
var apiKey = $"_ic_{hexString}_{checksum:x2}";
Console.WriteLine(apiKey);

// ex _ic_9fad2b4be649887c70a58b869c8838075b0dcf91554e64e2b367ba3079d079f5_fea
```

```csharp
// builder.Services.AddHealthChecks()
//                 .AddDbContextCheck<CatalogueDbContext>();

// builder.Services.AddApiKeyAuthentication(builder.Configuration);
// builder.Services.AddApiKeyAuthorization();

// builder.Logging.AddConsole();
```
