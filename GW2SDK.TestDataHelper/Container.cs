using System;
using System.Net;
using System.Net.Http;
using GW2SDK.Extensions;
using GW2SDK.Infrastructure.Common;
using Microsoft.Extensions.DependencyInjection;

namespace GW2SDK.TestDataHelper
{
    public class Container
    {
        private readonly IServiceProvider _services;

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
                    .AddTypedClient<JsonAchievementService>()
                    .AddTypedClient<JsonAchievementCategoriesService>()
                    .AddTypedClient<JsonBuildService>()
                    .AddTypedClient<JsonColorService>()
                    .AddTypedClient<JsonItemService>()
                    .AddTypedClient<JsonRecipeService>()
                    .AddTypedClient<JsonSkinService>()
                    .AddTypedClient<JsonWorldService>();
            _services = services.BuildServiceProvider();
        }

        public T Resolve<T>() => _services.GetRequiredService<T>();
    }
}
