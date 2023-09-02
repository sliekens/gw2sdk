using System;
using System.Net.Http;
using System.Threading.Tasks;
using GuildWars2.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;
using static System.Net.HttpStatusCode;

#if NETFRAMEWORK
using static GuildWars2.Http.HttpStatusCodeEx;
#endif

namespace GuildWars2.Tests.TestInfrastructure;

public class TestHttpClientFactory : IHttpClientFactory, IAsyncDisposable
{
    private readonly ServiceProvider httpClientProvider;

    public TestHttpClientFactory(Uri baseAddress)
    {
        httpClientProvider = BuildHttpClientProvider(baseAddress);
    }

    public async ValueTask DisposeAsync()
    {
        await httpClientProvider.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    public HttpClient CreateClient(string name) =>
        httpClientProvider.GetRequiredService<IHttpClientFactory>().CreateClient(name);

    /// <summary>Creates a service provider for the HTTP factory which is unfortunately very dependent on ServiceCollection.</summary>
    private static ServiceProvider BuildHttpClientProvider(Uri baseAddress)
    {
        const int maxConnections = 20;
        var services = new ServiceCollection();

        services.AddTransient<SchemaVersionHandler>();

        services.AddHttpClient(
                "GW2SDK",
                http =>
                {
                    http.BaseAddress = baseAddress;

                    // The default timeout is 100 seconds, but it's not always enough
                    // Requests can get stuck in a delayed retry-loop due to rate limiting
                    // A better solution might be to queue up requests
                    //   (so that new requests have to wait until there are no more delayed requests)
                    http.Timeout = TimeSpan.FromMinutes(5);
                }
            )
#if NET
            .ConfigurePrimaryHttpMessageHandler(
                () => new SocketsHttpHandler
                {
                    // Limit the number of open connections
                    //   because we have many tests trying to use the API concurrently,
                    //   resulting in a stupid amount of connections being opened
                    // The desired effect is to open a smaller number of connections that are reused often
                    MaxConnectionsPerServer = maxConnections,

                    // Creating a new connection shouldn't take more than 10 seconds
                    ConnectTimeout = TimeSpan.FromSeconds(10),

                    // Save bandwidth
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                }
            )
#else
            .ConfigurePrimaryHttpMessageHandler(
                () => new HttpClientHandler { MaxConnectionsPerServer = maxConnections }
            )
#endif
            .AddHttpMessageHandler<SchemaVersionHandler>()

            // The API has rate limiting (by IP address) so wait and retry when the server indicates too many requests
            .AddPolicyHandler(Policy.HandleResult<HttpResponseMessage>(response => response.StatusCode == TooManyRequests).WaitAndRetryAsync(10, _ => TimeSpan.FromSeconds(10)))

            // The API can be disabled intentionally to avoid leaking spoilers, or it can be unavailable due to technical difficulties
            // Since it's not easy to tell the difference, give it one retry
            .AddPolicyHandler(Policy.HandleResult<HttpResponseMessage>(response => response.StatusCode == ServiceUnavailable).RetryAsync())

            // Assume internal errors are retryable (within reason)
            .AddPolicyHandler(Policy.HandleResult<HttpResponseMessage>(response => response.StatusCode is InternalServerError or BadGateway or GatewayTimeout).RetryAsync(5))

            // Sometimes the API fails to validate the access token even though the token is valid
            // This is retryable because real token errors result in a different error message, eg. "Invalid access token"
            .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<UnauthorizedOperationException>(reason => reason.Message == "endpoint requires authentication").RetryAsync(10))

            // Sometimes the API returns a Bad Request with an unknown error for perfectly valid requests
            .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<ArgumentException>(reason => reason.Message == "unknown error").RetryAsync())

            // Abort each attempted request after max 30 seconds and perform retries (within reason)
            .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<TimeoutRejectedException>().RetryAsync(10))
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30), TimeoutStrategy.Optimistic));

        return services.BuildServiceProvider();
    }
}
