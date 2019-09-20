using System;
using System.Net;
using System.Net.Http;
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
    public class Container
    {
        private readonly IServiceProvider _services;

        public Container(string accessToken = null)
        {
            var services = new ServiceCollection();
            services.AddTransient(sp => new SocketsHttpHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            services.AddTransient<UnauthorizedMessageHandler>();
            services.AddTransient<BadMessageHandler>();
            services.AddTransient<RateLimitHandler>();
            var httpBuilder = services.AddHttpClient("GW2SDK",
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
                                      .AddTypedClient<AccountService>()
                                      .AddTypedClient<AccountAchievementService>()
                                      .AddTypedClient<AchievementService>()
                                      .AddTypedClient<AchievementCategoryService>()
                                      .AddTypedClient<AchievementGroupService>()
                                      .AddTypedClient<BuildService>()
                                      .AddTypedClient<ColorService>()
                                      .AddTypedClient<ContinentService>()
                                      .AddTypedClient<ItemService>()
                                      .AddTypedClient<ItemPriceService>()
                                      .AddTypedClient<RecipeService>()
                                      .AddTypedClient<SearchRecipeService>()
                                      .AddTypedClient<SkinService>()
                                      .AddTypedClient<SubtokenService>()
                                      .AddTypedClient<TokenInfoService>()
                                      .AddTypedClient<WorldService>();
            if (accessToken is string)
            {
                httpBuilder.ConfigureHttpClient(client => client.UseAccessToken(accessToken));
            }

            _services = services.BuildServiceProvider();
        }

        public T Resolve<T>() => _services.GetRequiredService<T>();
    }
}
