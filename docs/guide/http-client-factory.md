# HTTP client factories

Problems may arise from creating `new HttpClient()` instances with its parameterless constructor:

- Creating a _short-lived_ client each time can lead to socket exhaustion if you do it at scale
- Creating a _long-lived_ client that is reused can lead to stale DNS problems

To counter these issues, Microsoft recommends doing one of two things:

1. Either use a _long-lived_ client and set `PooledConnectionLifetime` to a reasonable value (e.g. 2 minutes)
2. Or obtain _short-lived_ clients from `IHttpClientFactory` which manages the lifetime of the underlying connections for you

## Example: using a long-lived client

In this example, a _long-lived_ client is created with a `PooledConnectionLifetime` of 2 minutes. The client is created once and reused for the lifetime of the application.

This might be the easiest way to safely use `HttpClient`.

``` csharp
var handler = new SocketsHttpHandler
{
    PooledConnectionLifetime = TimeSpan.FromMinutes(2)
};
var sharedClient = new HttpClient(handler);
```

> [!TIP]
> The shared `HttpClient` is never disposed in this case, which is on purpose. It's intended to stay alive for the lifetime of the application.

> [!NOTE]
> The `SocketsHttpHandler` is not available in .NET Framework, only in .NET Core and .NET 5+.

## Example: using Microsoft.Extensions.Http to manage the connection lifetime

In this example, a _short-lived_ client is obtained using `Microsoft.Extensions.Http`. The HTTP client factory manages the lifetime of the underlying connections for us. Disposing the `HttpClient` will not close the underlying connections, but it will make the connection available for reuse.

[!code-csharp[](../../samples/HttpClientFactoryUsage/Program.cs)]

## Additional sources

More details on the fundamentals of `HttpClient` can be found here:

- [Guidelines for using HttpClient](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines)
- [IHttpClientFactory with .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/httpclient-factory)

Guidance for ASP.NET Core applications:

- [Make HTTP requests using IHttpClientFactory in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests)