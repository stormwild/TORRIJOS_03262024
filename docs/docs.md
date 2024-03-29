# Docs or Notes

## Resources

[Setting up an Authorization Server with OpenIddict - Part I - Introduction - DEV Community](https://dev.to/robinvanderknaap/setting-up-an-authorization-server-with-openiddict-part-i-introduction-4jid)

[Setting up an Authorization Server with OpenIddict - Part II - Create ASPNET project - DEV Community](https://dev.to/robinvanderknaap/setting-up-an-authorization-server-with-openiddict-part-ii-create-aspnet-project-4949)

[Setting up an Authorization Server with OpenIddict - Part III - Client Credentials Flow - DEV Community](https://dev.to/robinvanderknaap/setting-up-an-authorization-server-with-openiddict-part-iii-client-credentials-flow-55lp)

[Setting up an Authorization Server with OpenIddict - Part IV - Authorization Code Flow - DEV Community](https://dev.to/robinvanderknaap/setting-up-an-authorization-server-with-openiddict-part-iv-authorization-code-flow-3eh8)

[Setting up an Authorization Server with OpenIddict - Part V - OpenID Connect - DEV Community](https://dev.to/robinvanderknaap/setting-up-an-authorization-server-with-openiddict-part-v-openid-connect-a8j)

[Setting up an Authorization Server with OpenIddict - Part VI - Refresh tokens - DEV Community](https://dev.to/robinvanderknaap/setting-up-an-authorization-server-with-openiddict-part-vi-refresh-tokens-5669)

[stormwild/authorization-server-openiddict: Authorization Server implemented with OpenIddict.](https://github.com/stormwild/authorization-server-openiddict)

[Setup Auth Server: OpenIddict](https://chat.openai.com/share/f7281293-5d99-4365-b08f-e29357f494d5)

[Set up token authentication with OpenIddict in .NET 5](https://nwb.one/blog/openid-connect-dotnet-5)

[Getting started with the OpenIddict web providers | Kévin Chalet's blog](https://kevinchalet.com/2022/12/16/getting-started-with-the-openiddict-web-providers/)

### Infrastructure

[Central Package Management | Microsoft Learn](https://learn.microsoft.com/en-us/nuget/consume-packages/central-package-management)

[Customize your build by folder or solution - MSBuild | Microsoft Learn](https://learn.microsoft.com/en-us/visualstudio/msbuild/customize-by-directory?view=vs-2022)

### Code Style

[C# code style by EditorConfig in .NET 5 SDK and beyond | Mews Developers](https://developers.mews.com/c-code-style-by-editorconfig-in-net-5-sdk-and-beyond/)

[c# - EditorConfig control File-scoped namespace declaration - Stack Overflow](https://stackoverflow.com/questions/69486362/editorconfig-control-file-scoped-namespace-declaration)

[namespace keyword - C# Reference - C# | Microsoft Learn](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/namespace)

[How to set C# 10+ file scoped namespaces as default in Visual Studio](https://davecallan.com/set-file-scoped-namespaces-default-dotnet6-visual-studio/)

### Build Props

[How to set C# 10+ file scoped namespaces as default in Visual Studio](https://davecallan.com/set-file-scoped-namespaces-default-dotnet6-visual-studio/)

## Health Checks

[Health checks in ASP.NET Core | Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/health-checks?view=aspnetcore-8.0)

```bash
dotnet add package Microsoft.Extensions.Diagnostics.HealthChecks.EntityFrameworkCore --version 8.0.3
```

[Health Checks in ASP.NET Core - Code Maze](https://code-maze.com/health-checks-aspnetcore/)

[Xabaril/AspNetCore.Diagnostics.HealthChecks: Enterprise HealthChecks for ASP.NET Core Diagnostics Package](https://github.com/Xabaril/AspNetCore.Diagnostics.HealthChecks)

## Middleware

[ASP.NET Core Middleware | Microsoft Learn](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-8.0)

> The order that middleware components are added in the `Program.cs` file defines the order in which the middleware components are invoked on requests and the reverse order for the response. The order is **critical** for security, performance, and functionality.
>
> The following highlighted code in `Program.cs` adds security-related middleware components in the typical recommended order:
>
> ```csharp
> using Microsoft.AspNetCore.Identity;
> using Microsoft.EntityFrameworkCore;
> using WebMiddleware.Data;
> 
> var builder = WebApplication.CreateBuilder(args);
> 
> var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
>     ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
> builder.Services.AddDbContext<ApplicationDbContext>(options =>
>     options.UseSqlServer(connectionString));
> builder.Services.AddDatabaseDeveloperPageExceptionFilter();
> 
> builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
>     .AddEntityFrameworkStores<ApplicationDbContext>();
> builder.Services.AddRazorPages();
> builder.Services.AddControllersWithViews();
> 
> var app = builder.Build();
> 
> if (app.Environment.IsDevelopment())
> {
>     app.UseMigrationsEndPoint();
> }
> else
> {
>     app.UseExceptionHandler("/Error");
>     app.UseHsts();
> }
> 
> app.UseHttpsRedirection();
> app.UseStaticFiles();
> // app.UseCookiePolicy();
> 
> app.UseRouting();
> // app.UseRateLimiter();
> // app.UseRequestLocalization();
> // app.UseCors();
> 
> app.UseAuthentication();
> app.UseAuthorization();
> // app.UseSession();
> // app.UseResponseCompression();
> // app.UseResponseCaching();
> 
> app.MapRazorPages();
> app.MapDefaultControllerRoute();
> 
> app.Run();
> ```

## Api Keys

[API Key Authentication Best Practices - YouTube](https://www.youtube.com/watch?v=ooyOmiczY1g)

### Api Key Middleware

[How To Write .NET Core Middleware with Minimal APIs? - YouTube](https://www.youtube.com/watch?v=yZ7ioK2yeXc)

[Implementing API Key Authentication in ASP.NET Core - YouTube](https://www.youtube.com/watch?v=GrJJXixjR8M)

[How to secure ASP.NET APIs using x-api-key API keys | by Josiah T Mahachi | Medium](https://medium.com/@josiahmahachi/secure-asp-net-apis-using-x-api-key-api-keys-62d63b2b9fb0)

Approaches: Middleware, Filter, Attribute

[AspNetCore Middleware · RicoSuter/NSwag Wiki](https://github.com/RicoSuter/NSwag/wiki/AspNetCore-Middleware#enable-api-key-authorization)

Enable API Key authentication

```csharp
services.AddOpenApiDocument(document => 
{
    document.AddSecurity("apikey", Enumerable.Empty<string>(), new OpenApiSecurityScheme
    {
        Type = OpenApiSecuritySchemeType.ApiKey,
        Name = "api_key",
        In = OpenApiSecurityApiKeyLocation.Header
    });

    document.OperationProcessors.Add(
        new AspNetCoreOperationSecurityScopeProcessor("apikey"));
//      new OperationSecurityScopeProcessor("apikey"));
});
```

[Implementing API Key Authentication in ASP.NET Core Using Attribute and Middleware | by Kefas Ogabi | Medium](https://medium.com/@kefasogabi/implementing-api-key-authentication-in-asp-net-core-using-attribute-and-middleware-dd57d76d9efa)

[Implementing API Key Authentication in ASP.NET Core - DEV Community](https://dev.to/me_janki/implementing-api-key-authentication-in-aspnet-core-5ch)

[Top 23 .NET Core Libraries that Every Developer Should Know](https://positiwise.com/blog/essential-net-core-libraries-that-every-programmer-should-know)

[How To Implement API Key Authentication In ASP.NET Core](https://www.milanjovanovic.tech/blog/how-to-implement-api-key-authentication-in-aspnet-core)

[Securing Asp.Net Core Minimal APIs with a custom Middleware](https://blog.jhonatanoliveira.dev/securing-aspnet-core-minimal-apis-with-a-custom-middleware)

[jhonatanfernando/api-fluent-validator](https://github.com/jhonatanfernando/api-fluent-validator)

[Working with custom authentication schemes in ASP.NET Core 8.0 | Matteo Contrini](https://matteosonoio.it/aspnet-core-authentication-schemes/)

[c# - ASP.NET core - simple API key authentication - Stack Overflow](https://stackoverflow.com/questions/70277577/asp-net-core-simple-api-key-authentication)

[Using API Key Authentication To Secure ASP.NET Core Web API](https://www.c-sharpcorner.com/article/using-api-key-authentication-to-secure-asp-net-core-web-api/)

## Structure

[Maybe it's time to rethink our project structure with .NET 6](https://timdeschryver.dev/blog/maybe-its-time-to-rethink-our-project-structure-with-dot-net-6)

> ```bash 
> WebApplication
> │   appsettings.json
> │   Program.cs
> │   WebApplication.csproj
> │
> ├───Modules
> │   └───Orders
> │       │   OrdersModule.cs
> │       ├───Models
> │       │       Order.cs
> │       └───Endpoints
> │               GetOrders.cs
> │               PostOrder.cs
> ```
