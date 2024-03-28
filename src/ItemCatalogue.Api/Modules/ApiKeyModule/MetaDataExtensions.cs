using Microsoft.AspNetCore.Mvc.Rendering;

namespace ItemCatalogue.Api.Modules.ApiKeyModule;

[AttributeUsage(AttributeTargets.Method)]
public class RequireApiKeyAttribute : Attribute
{
}

public static class MetaDataExtensions
{
    public static TBuilder RequireApiKey<TBuilder>(this TBuilder builder) where TBuilder : IEndpointConventionBuilder
    {
        builder.Add(endpointBuilder => endpointBuilder.Metadata.Add(new RequireApiKeyAttribute()));

        return builder;
    }

}
