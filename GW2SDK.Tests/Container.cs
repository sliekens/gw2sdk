using System;
using System.Net;
using System.Net.Http;
using GW2SDK.Extensions;
using GW2SDK.Features.Accounts;
using GW2SDK.Features.Accounts.Achievements;
using GW2SDK.Features.Achievements;
using GW2SDK.Features.Achievements.Categories;
using GW2SDK.Features.Achievements.Groups;
using GW2SDK.Features.Builds;
using GW2SDK.Features.Colors;
using GW2SDK.Features.Items;
using GW2SDK.Features.Recipes;
using GW2SDK.Features.Recipes.Search;
using GW2SDK.Features.Skins;
using GW2SDK.Features.Subtokens;
using GW2SDK.Features.Tokens;
using GW2SDK.Features.Worlds;
using GW2SDK.Infrastructure.Common;
using GW2SDK.Tests.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace GW2SDK.Tests
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
                                      .AddTypedClient<ItemService>()
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
