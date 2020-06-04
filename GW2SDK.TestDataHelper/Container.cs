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
            services.AddTransient(sp => new SocketsHttpHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
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
                .ConfigurePrimaryHttpMessageHandler(sp => sp.GetRequiredService<SocketsHttpHandler>())
                .AddPolicyHandler(HttpPolicy.SelectPolicy)
                .AddHttpMessageHandler<UnauthorizedMessageHandler>()
                .AddHttpMessageHandler<BadMessageHandler>()
                .AddHttpMessageHandler<RateLimitHandler>()
                .AddTypedClient(http => new ContinentService(http))
                .AddTypedClient(http => new JsonAchievementService(http))
                .AddTypedClient(http => new JsonAchievementCategoriesService(http))
                .AddTypedClient(http => new JsonAchievementGroupsService(http))
                .AddTypedClient(http => new JsonBuildService(http))
                .AddTypedClient(http => new JsonColorService(http))
                .AddTypedClient(http => new JsonContinentService(http))
                .AddTypedClient(http => new JsonFloorService(http))
                .AddTypedClient(http => new JsonItemService(http))
                .AddTypedClient(http => new JsonItemPriceService(http))
                .AddTypedClient(http => new JsonRecipeService(http))
                .AddTypedClient(http => new JsonSkinService(http))
                .AddTypedClient(http => new JsonWorldService(http));
            _services = services.BuildServiceProvider();
        }

        public ValueTask DisposeAsync() => _services.DisposeAsync();

        public void Dispose() => _services.Dispose();

        public T Resolve<T>() => _services.GetRequiredService<T>();
    }
}
