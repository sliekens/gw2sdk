using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts;
using GW2SDK.Accounts.Achievements;
using GW2SDK.Achievements;
using GW2SDK.Achievements.Categories;
using GW2SDK.Achievements.Groups;
using GW2SDK.Builds;
using GW2SDK.Colors;
using GW2SDK.Commerce.Prices;
using GW2SDK.Continents;
using GW2SDK.Extensions;
using GW2SDK.Impl.HttpMessageHandlers;
using GW2SDK.Items;
using GW2SDK.Recipes;
using GW2SDK.Recipes.Search;
using GW2SDK.Skins;
using GW2SDK.Subtokens;
using GW2SDK.Tokens;
using GW2SDK.Worlds;
using Microsoft.Extensions.DependencyInjection;

namespace GW2SDK.Tests.TestInfrastructure
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
                        http.UseBaseAddress(ConfigurationManager.Instance.BaseAddress);
                        http.UseLatestSchemaVersion();
                        http.UseDataCompression();
                    })
                .ConfigurePrimaryHttpMessageHandler(sp => sp.GetRequiredService<SocketsHttpHandler>())
                .AddPolicyHandler(HttpPolicy.SelectPolicy)
                .AddHttpMessageHandler<UnauthorizedMessageHandler>()
                .AddHttpMessageHandler<BadMessageHandler>()
                .AddHttpMessageHandler<RateLimitHandler>()
                .AddTypedClient(http => new AccountService(http))
                .AddTypedClient(http => new AccountAchievementService(http))
                .AddTypedClient(http => new AchievementService(http))
                .AddTypedClient(http => new AchievementCategoryService(http))
                .AddTypedClient(http => new AchievementGroupService(http))
                .AddTypedClient(http => new BuildService(http))
                .AddTypedClient(http => new ColorService(http))
                .AddTypedClient(http => new ContinentService(http))
                .AddTypedClient(http => new ItemService(http))
                .AddTypedClient(http => new ItemPriceService(http))
                .AddTypedClient(http => new RecipeService(http))
                .AddTypedClient(http => new SearchRecipeService(http))
                .AddTypedClient(http => new SkinService(http))
                .AddTypedClient(http => new SubtokenService(http))
                .AddTypedClient(http => new TokenInfoService(http))
                .AddTypedClient(http => new WorldService(http));

            _services = services.BuildServiceProvider();
        }

        public ValueTask DisposeAsync() => _services.DisposeAsync();

        public void Dispose() => _services.Dispose();

        public T Resolve<T>() => _services.GetRequiredService<T>();
    }
}
