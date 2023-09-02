using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;
using static System.Net.HttpStatusCode;

namespace GuildWars2.TestDataHelper;

public class Container : IDisposable, IAsyncDisposable
{
    private readonly ServiceProvider serviceProvider;

    public Container()
    {
        var services = new ServiceCollection();
        services.AddHttpClient(
                "com.guildwars2.api",
                client =>
                {
                    client.BaseAddress = BaseAddress.DefaultUri;

                    // The default timeout is 100 seconds, but it's not always enough
                    // Due to rate limiting, an individual request can get stuck in a delayed retry-loop
                    // A better solution might be to queue up requests
                    //   (so that new requests have to wait until there are no more delayed requests)
                    client.Timeout = TimeSpan.FromMinutes(5);
                }
            )
            .ConfigurePrimaryHttpMessageHandler(
                () => new SocketsHttpHandler
                {
                    MaxConnectionsPerServer = 20,

                    // Creating a new connection shouldn't take more than 10 seconds
                    ConnectTimeout = TimeSpan.FromSeconds(10),

                    // Save bandwidth
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip
                }
            )
            // The API has rate limiting (by IP address) so wait and retry when the server indicates too many requests
            .AddPolicyHandler(Policy.HandleResult<HttpResponseMessage>(response => response.StatusCode == TooManyRequests).WaitAndRetryAsync(10, _ => TimeSpan.FromSeconds(10)))

            // The API can be disabled intentionally to avoid leaking spoilers, or it can be unavailable due to technical difficulties
            // Since it's not easy to tell the difference, give it one retry
            .AddPolicyHandler(Policy.HandleResult<HttpResponseMessage>(response => response.StatusCode == ServiceUnavailable).RetryAsync())

            // Assume internal errors are retryable (within reason)
            .AddPolicyHandler(Policy.HandleResult<HttpResponseMessage>(response => response.StatusCode is InternalServerError or BadGateway or GatewayTimeout).RetryAsync(5))

            // Sometimes the API returns a Bad Request with an unknown error for perfectly valid requests
            .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<ArgumentException>(reason => reason.Message == "unknown error").RetryAsync())

            // Abort each attempted request after max 30 seconds and perform retries (within reason)
            .AddPolicyHandler(Policy<HttpResponseMessage>.Handle<TimeoutRejectedException>().RetryAsync(10))
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(30), TimeoutStrategy.Optimistic))
            .AddTypedClient<JsonAchievementService>()
            .AddTypedClient<JsonItemPriceService>()
            .AddTypedClient<JsonOrderBookService>()
            .AddTypedClient<JsonItemService>()
            .AddTypedClient<JsonRecipeService>()
            .AddTypedClient<JsonSkinService>();

        serviceProvider = services.BuildServiceProvider();
    }

    public ValueTask DisposeAsync() => serviceProvider.DisposeAsync();

    public void Dispose() => serviceProvider.Dispose();

    public T Resolve<T>() where T : notnull => serviceProvider.GetRequiredService<T>();
}
