using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Polly;
using Polly.Hedging;
using Polly.Retry;
using Polly.Timeout;
using static System.Net.HttpStatusCode;

namespace GuildWars2.TestDataHelper;

public class Container : IDisposable, IAsyncDisposable
{
    private readonly ServiceProvider serviceProvider;

    public Container()
    {
        var services = new ServiceCollection();
        var httpClientBuilder = services.AddHttpClient("com.guildwars2.api")
            .AddTypedClient<JsonAchievementService>()
            .AddTypedClient<JsonItemService>()
            .AddTypedClient<JsonRecipeService>()
            .AddTypedClient<JsonSkinService>();

        httpClientBuilder.ConfigurePrimaryHttpMessageHandler(
            () => new SocketsHttpHandler
            {
                MaxConnectionsPerServer = 1000,

                // Creating a new connection shouldn't take more than 10 seconds
                ConnectTimeout = TimeSpan.FromSeconds(10)
            }
        );

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

                            async Task<bool> IsUnknownError(
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

        serviceProvider = services.BuildServiceProvider();
    }

    public ValueTask DisposeAsync() => serviceProvider.DisposeAsync();

    public void Dispose() => serviceProvider.Dispose();

    public T Resolve<T>() where T : notnull => serviceProvider.GetRequiredService<T>();
}
