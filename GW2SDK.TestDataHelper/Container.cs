using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Continents;
using GW2SDK.Extensions;
using GW2SDK.Impl.HttpMessageHandlers;
using Microsoft.Extensions.DependencyInjection;

namespace GW2SDK.TestDataHelper
{
    public class Container : IDisposable, IAsyncDisposable
    {
        private readonly ServiceProvider _services;

        public Container()
        {
            var services = new ServiceCollection();
            services.AddTransient<UnauthorizedMessageHandler>();
            services.AddTransient<BadMessageHandler>();
            services.AddTransient<RateLimitHandler>();
            services.AddHttpClient("GW2SDK",
                    http =>
                    {
                        http.UseBaseAddress(new Uri("https://api.guildwars2.com", UriKind.Absolute));
                        http.UseLatestSchemaVersion();
                        http.UseDataCompression();
                    })
                .ConfigurePrimaryHttpMessageHandler(() =>
                    new SocketsHttpHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })
                .AddPolicyHandler(HttpPolicy.SelectPolicy)
                .AddHttpMessageHandler<UnauthorizedMessageHandler>()
                .AddHttpMessageHandler<BadMessageHandler>()
                .AddHttpMessageHandler<RateLimitHandler>()
                .AddTypedClient<ContinentService>()
                .AddTypedClient<JsonAchievementService>()
                .AddTypedClient<JsonAchievementCategoriesService>()
                .AddTypedClient<JsonAchievementGroupsService>()
                .AddTypedClient<JsonBuildService>()
                .AddTypedClient<JsonColorService>()
                .AddTypedClient<JsonContinentService>()
                .AddTypedClient<JsonFloorService>()
                .AddTypedClient<JsonItemService>()
                .AddTypedClient<JsonItemPriceService>()
                .AddTypedClient<JsonRecipeService>()
                .AddTypedClient<JsonSkinService>()
                .AddTypedClient<JsonWorldService>();
            _services = services.BuildServiceProvider();
        }

        public ValueTask DisposeAsync() => _services.DisposeAsync();

        public void Dispose() => _services.Dispose();

        public T Resolve<T>() => _services.GetRequiredService<T>();
    }
}
