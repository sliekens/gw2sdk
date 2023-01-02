using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GuildWars2.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;
using static System.Net.HttpStatusCode;

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
                    // Due to rate limiting, an individual request can get stuck in a delayed retry-loop
                    // A better solution might be to queue up requests
                    //   (so that new requests have to wait until there are no more delayed requests)
                    // Perhaps a circuit breaker is also suitable
                    http.Timeout = TimeSpan.FromMinutes(5);
                }
            )
#if NETCOREAPP
            .ConfigurePrimaryHttpMessageHandler(
                () => new SocketsHttpHandler
                {
                    // Limit the number of open connections
                    //   because we have many tests trying to use the API concurrently,
                    //   resulting in a stupid amount of connections being opened
                    // The desired effect is to open a smaller number of connections that are reused often
                    MaxConnectionsPerServer = maxConnections,

                    // Creating a new connection shouldn't take more than 10 seconds
                    ConnectTimeout = TimeSpan.FromSeconds(10)
                }
            )
#else
            .ConfigurePrimaryHttpMessageHandler(
                () => new HttpClientHandler { MaxConnectionsPerServer = maxConnections }
            )
#endif
            .AddHttpMessageHandler<SchemaVersionHandler>()
            .AddPolicyHandler(

                // Transient errors for which we want a a delayed retry
                // (currently only includes rate-limit errors)
                Policy<HttpResponseMessage>.HandleResult(
                        response => response.StatusCode is (HttpStatusCode)429 // TooManyRequests
                    )
                    .WaitAndRetryForeverAsync(_ => TimeSpan.FromSeconds(10))
            )
            .AddPolicyHandler(

                // Transient errors for which we want an immediate retry
                Policy<HttpResponseMessage>
                    .HandleResult(response => response.StatusCode >= InternalServerError)
                    .Or<TimeoutRejectedException>()
                    .Or<UnauthorizedOperationException>(

                        // Sometimes the API fails to validate the access key
                        // This is a server error, real token problems result in a different error
                        // eg. "Invalid access token"
                        reason => reason.Message == "endpoint requires authentication"
                    )
                    .Or<ArgumentException>(

                        // Sometimes the API returns a Bad Request with an unknown error for perfectly valid requests
                        reason => reason.Message == "unknown error"
                    )
                    .RetryForeverAsync()
            )
            .AddPolicyHandler(

                // An individual attempt shouldn't take more than 20 seconds to complete
                // Assume longer means the API is stuck, and we should cancel the request
                //
                Policy.TimeoutAsync<HttpResponseMessage>(
                    TimeSpan.FromSeconds(20),
                    TimeoutStrategy.Optimistic
                )
            );

        return services.BuildServiceProvider();
    }
}
