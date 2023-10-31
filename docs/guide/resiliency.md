# Adding resiliency to your app

There exists a wide category of network errors that you should handle in your application.

GW2SDK by default does not handle any network errors, because it is a complex topic and the solution depends on your application's needs.

## Type of errors

In addition to the rate limit errors already discussed, expect other errors to occur as well:

- DNS errors
- Connection timeouts or interruptions
- API offline for maintenance
- API offline to prevent spoilers for new content
- API offline due to other reasons
- API returning internal server errors
- API returning unexpected data like HTML or XML instead of JSON

Errors may also occur when deserializing the JSON (unmarshaling) to objects, which is explained in a bit more detail at the end.

## Use Polly

To counter network problems, make your HttpClient more resilient with automatic retries, timeout and delay policies using Polly.

I recommend at least adding some timeouts and automatic retries.

This example has the following policies:

1. A Retry policy that performs delayed retries when the rate limit is exceeded or when there is a Service Unavailable error.
2. A Hedging policy that performs an immediate retry when there is an internal server error or a gateway timeout.

The example uses `Microsoft.Extensions.Http.Resilience` but it's not a requirement to use Polly. You could instead create a custom `DelegatingHandler` with `ResiliencePipelineBuilder<HttpResponseMessage>`.

Tweak the `Gw2Resiliency` options as needed.

[!code-csharp[](../../samples/PollyUsage/Program.cs)]

## Additional resources

- [Build resilient HTTP apps: Key development patterns](https://learn.microsoft.com/en-us/dotnet/core/resilience/http-resilience)
- [Use IHttpClientFactory to implement resilient HTTP requests](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests) (outdated code, but the concepts are still valid)
