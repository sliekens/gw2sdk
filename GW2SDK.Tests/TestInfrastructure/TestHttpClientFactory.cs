using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.Hedging;
using Polly.Retry;
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
        var services = new ServiceCollection();

        services.AddTransient<SchemaVersionHandler>();

        var httpClientBuilder = services.AddHttpClient(
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
        );
#if NET
        httpClientBuilder.ConfigurePrimaryHttpMessageHandler(
            () => new SocketsHttpHandler
            {
                // Limit the number of open connections
                //   because we have many tests trying to use the API concurrently,
                //   resulting in a stupid amount of connections being opened
                // The desired effect is to open a smaller number of connections that are reused often
                MaxConnectionsPerServer = 100,

                // Creating a new connection shouldn't take more than 10 seconds
                ConnectTimeout = TimeSpan.FromSeconds(10)
            }
        );
#else
        httpClientBuilder.ConfigurePrimaryHttpMessageHandler(
            () => new HttpClientHandler { MaxConnectionsPerServer = 100 }
        );
#endif

        httpClientBuilder.AddHttpMessageHandler<SchemaVersionHandler>();

        httpClientBuilder.AddResilienceHandler(
            "api.guildwars2.com",
            builder =>
            {
                builder.AddRetry(
                    new RetryStrategyOptions<HttpResponseMessage>
                    {
                        ShouldHandle = attempt => attempt.Outcome switch
                        {
                            // Retry on too many requests
                            { Result.StatusCode: TooManyRequests } => PredicateResult.True(),

                            // Retry on Service Unavailable just once
                            // because we don't know if it's intentional or due to technical difficulties
                            { Result.StatusCode: ServiceUnavailable } when attempt.AttemptNumber
                                == 0 => PredicateResult.True(),

                            _ => PredicateResult.False()
                        },
                        MaxRetryAttempts = 100,
                        BackoffType = DelayBackoffType.Constant,
                        Delay = TimeSpan.FromSeconds(10),
                        UseJitter = true
                    }
                );

                // API can be slow or misbehave, use a hedging strategy to retry without delay
                builder.AddHedging(
                    new HedgingStrategyOptions<HttpResponseMessage>
                    { 
                        // If no response is received within 30 seconds, abort the in-flight request and retry
                        Delay = TimeSpan.FromSeconds(30),
                        ShouldHandle = async attempt =>
                        {
                            return attempt.Outcome switch
                            {
                                { Result.IsSuccessStatusCode: true } => false,

                                // The following replies are considered retryable without a back-off delay
                                {
                                    Result.StatusCode: InternalServerError
                                    or BadGateway
                                    or GatewayTimeout
                                } => true,

                                // Sometimes the API returns weird data, also treat as internal errors
                                _ when await IsUnknownError(attempt) => true,

                                _ => false
                            };

                            static async Task<bool> IsUnknownError(
                                HedgingPredicateArguments<HttpResponseMessage> attempt
                            )
                            {
                                if (attempt.Outcome.Result is null)
                                {
                                    return false;
                                }

                                if (attempt.Outcome.Result.Content.Headers.ContentType?.MediaType
                                    != "application/json")
                                {
                                    return true;
                                }

                                // IMPORTANT: buffer the content so it can be read multiple times if needed
                                await attempt.Outcome.Result.Content.LoadIntoBufferAsync();

                                // ALSO IMPORTANT: do not dispose the content stream
                                var content =
                                    await attempt.Outcome.Result.Content.ReadAsStreamAsync();
                                try
                                {
                                    using var json = await JsonDocument.ParseAsync(content);
                                    if (!json.RootElement.TryGetProperty("text", out var text))
                                    {
                                        return true;
                                    }

                                    // Sometimes you get an authentication error even though your API key is valid
                                    // Treat this message as an internal error, because you get a different message if the API key is really invalid
                                    return text.GetString() is "endpoint requires authentication"
                                        or "unknown error"
                                        or "ErrBadData"
                                        or "ErrTimeout";
                                }
                                finally
                                {
                                    content.Position = 0;
                                }
                            }
                        }
                    }
                );
            }
        );

        return services.BuildServiceProvider();
    }
}
