# Managing the lifetime of HttpClient

Problems may arise from creating `new HttpClient()` instances with its parameterless constructor:

- Creating a _short-lived_ client each time can lead to socket exhaustion if you do it at scale
- Creating a _long-lived_ client that is reused can lead to stale DNS problems

To counter these issues, Microsoft recommends doing one of two things:

1. Either use a _long-lived_ client and set `PooledConnectionLifetime` to a reasonable value (e.g. 15 minutes)
2. Or obtain _short-lived_ clients from `IHttpClientFactory` which manages the lifetime of the underlying connections for you

## Example: using a long-lived client

In this example, a _long-lived_ client is created with a `PooledConnectionLifetime` of 15 minutes. The client is created once and reused for the lifetime of the application.

> [!WARNING]
> This solution is unavailable for .NET Framework, the `SocketsHttpHandler` only exists for .NET Core and .NET 5+.

This is a simpler way to safely use `HttpClient` compared to using `Microsoft.Extensions.Http`.

``` csharp
var handler = new SocketsHttpHandler
{
    PooledConnectionLifetime = TimeSpan.FromMinutes(15)
};
var sharedClient = new HttpClient(handler);
```

> [!NOTE]
> The shared `HttpClient` is never disposed in this case, which is on purpose. It's intended to stay alive for the lifetime of the application.

To use Polly with this simplified usage pattern, see [Resilience with static clients](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines#resilience-with-static-clients) for the recommended approach.

Here is an example of how to configure Polly with a static client:

(You can find the `Gw2Resiliency` code in the samples directory.)

``` csharp
using System;
using System.Net.Http;
using Microsoft.Extensions.Http;
using Microsoft.Extensions.Http.Resilience;
using Polly;

var resiliencePipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
    .AddTimeout(Gw2Resiliency.TotalTimeoutStrategy) // configure the total timeout for the entire request, including retries
    .AddRetry(Gw2Resiliency.RetryStrategy) // configure delayed retries for  failed requests
    .AddCircuitBreaker(Gw2Resiliency.CircuitBreakerStrategy) // configure blocking requests after too many failures
    .AddHedging(Gw2Resiliency.HedgingStrategy) // configure hedging for slow requests and for certain errors
    .AddTimeout(Gw2Resiliency.AttemptTimeoutStrategy); // configure the timeout for each individual request attempt
    .Build();

var primaryHandler = new SocketsHttpHandler
{
    PooledConnectionLifetime = TimeSpan.FromMinutes(15)
};

var resilienceHandler = new ResilienceHandler(resiliencePipeline)
{
    InnerHandler = primaryHandler,
};

var httpClient = new HttpClient(resilienceHandler);
```

## Example: using Microsoft.Extensions.Http to manage the connection lifetime

You already saw an example of this on the previous page about resiliency with Polly, but here's another example that focuses on the lifetime management.

In this example, a _short-lived_ client is obtained using `Microsoft.Extensions.Http`. The HTTP client factory manages the lifetime of the underlying connections for us. Disposing the `HttpClient` will not close the underlying connections, but it will make the connection available for reuse.

[!code-csharp[](../../samples/HttpClientFactoryUsage/Program.cs)]

## Additional resources

More details on the fundamentals of `HttpClient` can be found here:

- [Guidelines for using HttpClient](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines)
- [IHttpClientFactory with .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/httpclient-factory)

Guidance for ASP.NET Core applications:

- [Make HTTP requests using IHttpClientFactory in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests)
