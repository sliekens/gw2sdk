using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;
using static System.Net.HttpStatusCode;

namespace GW2SDK.TestDataHelper;

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
                }
            )
            .AddTypedClient<JsonAchievementService>()
            .AddTypedClient<JsonItemPriceService>()
            .AddTypedClient<JsonOrderBookService>()
            .AddTypedClient<JsonItemService>()
            .AddTypedClient<JsonRecipeService>()
            .AddTypedClient<JsonSkinService>()
            .ConfigurePrimaryHttpMessageHandler(
                () => new SocketsHttpHandler { AutomaticDecompression = DecompressionMethods.GZip }
            )
            .AddPolicyHandler(
                Policy.TimeoutAsync<HttpResponseMessage>(
                    TimeSpan.FromSeconds(100),
                    TimeoutStrategy.Optimistic
                )
            )
            .AddPolicyHandler(
                Policy<HttpResponseMessage>.HandleResult(
                        response => response.StatusCode is ServiceUnavailable
                            or GatewayTimeout
                            or BadGateway
                            or (HttpStatusCode)429 // TooManyRequests
                    )
                    .Or<TimeoutRejectedException>()
                    .WaitAndRetryForeverAsync(
                        retryAttempt => TimeSpan.FromSeconds(Math.Min(8, Math.Pow(2, retryAttempt)))
                    )
            )
            .AddPolicyHandler(
                Policy.TimeoutAsync<HttpResponseMessage>(
                    TimeSpan.FromSeconds(30),
                    TimeoutStrategy.Optimistic
                )
            );
        ;

        serviceProvider = services.BuildServiceProvider();
    }

    public ValueTask DisposeAsync() => serviceProvider.DisposeAsync();

    public void Dispose() => serviceProvider.Dispose();

    public T Resolve<T>() where T : notnull => serviceProvider.GetRequiredService<T>();
}
