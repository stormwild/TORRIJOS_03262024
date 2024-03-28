
using System.Diagnostics;
using System.Text.Json;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ItemCatalogue.Api.Modules.ApiKeyModule;

public class ApiKeyAuthorization : IMiddleware
{
    private readonly IOptions<AuthenticationSettings> _authenticationSettings;
    private readonly ILogger<ApiKeyAuthorization> _logger;

    public ApiKeyAuthorization(ILogger<ApiKeyAuthorization> logger, IOptions<AuthenticationSettings> authenticationSettings)
    {
        _logger = logger;
        _authenticationSettings = authenticationSettings;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        if (context.GetEndpoint()?.Metadata.GetMetadata<RequireApiKeyAttribute>() is null)
        {
            await next(context);

            return;
        }

        if (!context.Request.Headers.TryGetValue(ApiKeyConstants.ApiKeyHeaderName, out var apiKey))
        {
            _logger.LogWarning(ApiKeyConstants.ApiKeyNotFound);

            await SetUnauthorizedResponse(context, ApiKeyConstants.ApiKeyNotFound);

            return;
        }

        if (apiKey != _authenticationSettings.Value.ApiKey)
        {
            _logger.LogWarning(ApiKeyConstants.ApiKeyInvalid);

            await SetUnauthorizedResponse(context, ApiKeyConstants.ApiKeyInvalid);

            return;
        }

        await next(context);
    }

    private static async Task SetUnauthorizedResponse(HttpContext context, string message = ApiKeyConstants.ApiKeyError)
    {
        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(JsonSerializer.Serialize(new ProblemDetails
        {
            Title = "Unauthorized",
            Detail = message,
            Status = StatusCodes.Status401Unauthorized,
        }));
    }
}
