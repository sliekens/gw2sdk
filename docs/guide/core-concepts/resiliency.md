# Adding Resiliency

GW2SDK doesn't handle network errors by default—the right strategy depends on your app.

---

## 🔴 Types of Errors

| Category | Examples |
|----------|----------|
| **Network** | DNS failures, connection timeouts, interruptions |
| **API Downtime** | Maintenance, spoiler prevention, unplanned outages |
| **Server Errors** | 500 Internal Server Error, 503 Service Unavailable |
| **Unexpected Response** | HTML/XML instead of JSON |
| **Deserialization** | JSON doesn't match expected schema |

---

## 🛡️ Using Polly for Resilience

Add automatic retries, timeouts, and circuit breakers with [Polly](https://www.thepollyproject.org/).

### Recommended Strategies

| Strategy | Purpose |
|----------|----------|
| **Retry** | Delayed retries for rate limits (429) and Service Unavailable (503) |
| **Hedging** | Immediate retry for slow responses, 500, or 504 errors |
| **Timeout** | Prevent requests from hanging indefinitely |
| **Circuit Breaker** | Stop requests temporarily after repeated failures |

### Example

[!code-csharp[](~/samples/PollyUsage/Program.cs)]

> [!TIP]
> Customize `Gw2Resiliency` options for your needs. You can also use `ResiliencePipelineBuilder<HttpResponseMessage>` directly without `Microsoft.Extensions.Http.Resilience`.

---

## 📚 Additional Resources

| Resource | Description |
|----------|-------------|
| [Build resilient HTTP apps](https://learn.microsoft.com/en-us/dotnet/core/resilience/http-resilience) | Microsoft's official patterns guide |
| [IHttpClientFactory patterns](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests) | Factory-based resilience (concepts still valid) |
