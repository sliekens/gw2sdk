# Managing HttpClient Lifetime

Improper `HttpClient` usage can cause issues:

| Problem | Cause |
|---------|-------|
| **Socket exhaustion** | Creating many short-lived clients |
| **Stale DNS** | Single long-lived client without connection recycling |

---

## ✅ Recommended Approaches

### Option 1: Long-lived Client with Connection Recycling

Create one client and set `PooledConnectionLifetime`:

```csharp
var handler = new SocketsHttpHandler
{
    PooledConnectionLifetime = TimeSpan.FromMinutes(15)
};
var sharedClient = new HttpClient(handler);
```

> [!WARNING]
> `SocketsHttpHandler` is only available in .NET Core 2.1+ and .NET 5+. Not available for .NET Framework.

> [!NOTE]
> Don't dispose this client—it's designed to live for the entire application lifetime.

#### Adding Polly Resilience

```csharp
using Microsoft.Extensions.Http.Resilience;
using Polly;

var resiliencePipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
    .AddTimeout(Gw2Resiliency.TotalTimeoutStrategy)
    .AddRetry(Gw2Resiliency.RetryStrategy)
    .AddCircuitBreaker(Gw2Resiliency.CircuitBreakerStrategy)
    .AddHedging(Gw2Resiliency.HedgingStrategy)
    .AddTimeout(Gw2Resiliency.AttemptTimeoutStrategy)
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

> See [Resilience with static clients](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines#resilience-with-static-clients) for more details.

---

### Option 2: IHttpClientFactory

Let the factory manage connection lifetimes. Disposing the `HttpClient` returns connections to the pool.

[!code-csharp[](~/samples/HttpClientFactoryUsage/Program.cs)]

---

## 📚 Additional Resources

| Resource | Description |
|----------|-------------|
| [HttpClient Guidelines](https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines) | Microsoft's official best practices |
| [IHttpClientFactory with .NET](https://learn.microsoft.com/en-us/dotnet/core/extensions/httpclient-factory) | Factory pattern deep dive |

Guidance for ASP.NET Core applications:

- [Make HTTP requests using IHttpClientFactory in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests)
