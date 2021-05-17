using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Continents;
using GW2SDK.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;

namespace GW2SDK.TestDataHelper
{
    public class Container : IDisposable, IAsyncDisposable
    {
        private static readonly Random Jitterer = new Random();
        private readonly ServiceProvider _services;

        public Container()
        {
            var services = new ServiceCollection();
            var policies = services.AddPolicyRegistry();
            var innerTimeout = Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(10), TimeoutStrategy.Optimistic);
            var immediateRetry = Policy.Handle<HttpRequestException>()
                .Or<TimeoutException>()
                .Or<TimeoutRejectedException>()
                .OrResult<HttpResponseMessage>(r => (int) r.StatusCode >= 500).RetryForeverAsync();
            var rateLimit = Policy.Handle<TooManyRequestsException>()
                .WaitAndRetryForeverAsync(retryAttempt =>
                    TimeSpan.FromSeconds(Math.Min(8, Math.Pow(2, retryAttempt))) + TimeSpan.FromMilliseconds(Jitterer.Next(0, 1000)));
            policies.Add("Http",           rateLimit.WrapAsync(innerTimeout));
            policies.Add("HttpIdempotent", rateLimit.WrapAsync(immediateRetry).WrapAsync(innerTimeout));
            services.AddTransient<UnauthorizedMessageHandler>();
            services.AddTransient<BadMessageHandler>();
            services.AddTransient<RateLimitHandler>();
            services.AddHttpClient("GW2SDK",
                    http =>
                    {
                        http.BaseAddress = new Uri("https://api.guildwars2.com", UriKind.Absolute);
                        http.UseSchemaVersion(SchemaVersion.Latest);
                    })
                .ConfigurePrimaryHttpMessageHandler(() =>
                    new SocketsHttpHandler { MaxConnectionsPerServer = 8, AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })
                .AddPolicyHandlerFromRegistry((registry, message) => message.Method == HttpMethod.Post || message.Method == HttpMethod.Patch
                    ? registry.Get<IAsyncPolicy<HttpResponseMessage>>("Http")
                    : registry.Get<IAsyncPolicy<HttpResponseMessage>>("HttpIdempotent"))
                .AddHttpMessageHandler<UnauthorizedMessageHandler>()
                .AddHttpMessageHandler<BadMessageHandler>()
                .AddHttpMessageHandler<RateLimitHandler>()
                .AddTypedClient<ContinentService>()
                .AddTypedClient<JsonAchievementService>()
                .AddTypedClient<JsonAchievementCategoriesService>()
                .AddTypedClient<JsonAchievementGroupsService>()
                .AddTypedClient<JsonApiInfoService>()
                .AddTypedClient<JsonBuildService>()
                .AddTypedClient<JsonColorService>()
                .AddTypedClient<JsonContinentService>()
                .AddTypedClient<JsonCurrencyService>()
                .AddTypedClient<JsonFloorService>()
                .AddTypedClient<JsonItemPriceService>()
                .AddTypedClient<JsonItemListingService>()
                .AddTypedClient<JsonItemService>()
                .AddTypedClient<JsonMailCarriersService>()
                .AddTypedClient<JsonRecipeService>()
                .AddTypedClient<JsonSkinService>()
                .AddTypedClient<JsonTitlesService>()
                .AddTypedClient<JsonTraitsService>()
                .AddTypedClient<JsonWorldService>()
                .ConfigureHttpClient(c =>
                {
                });
            _services = services.BuildServiceProvider();
        }

        public ValueTask DisposeAsync() => _services.DisposeAsync();

        public void Dispose() => _services.Dispose();

        public T Resolve<T>() where T : notnull => _services.GetRequiredService<T>();
    }
}
