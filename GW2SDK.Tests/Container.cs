using System;
using System.Net.Http;
using GW2SDK.Extensions;
using GW2SDK.Features.Accounts;
using GW2SDK.Features.Accounts.Achievements;
using GW2SDK.Features.Achievements;
using GW2SDK.Features.Builds;
using GW2SDK.Features.Colors;
using GW2SDK.Features.Items;
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
            services.AddTransient<SocketsHttpHandler>();
            services.AddTransient<UnauthorizedMessageHandler>();
            services.AddTransient<BadMessageHandler>();
            services.AddTransient<RateLimitHandler>();
            var httpBuilder = services.AddHttpClient("GW2SDK",
                                          http =>
                                          {
                                              http.UseBaseAddress(ConfigurationManager.Instance.BaseAddress);
                                              http.UseLatestSchemaVersion();
                                          })
                                      .ConfigurePrimaryHttpMessageHandler(sp => sp.GetRequiredService<SocketsHttpHandler>())
                                      .AddHttpMessageHandler<UnauthorizedMessageHandler>()
                                      .AddHttpMessageHandler<BadMessageHandler>()
                                      .AddHttpMessageHandler<RateLimitHandler>()
                                      .AddPolicyHandler(HttpPolicy.SelectPolicy)
                                      .AddTypedClient<AccountService>()
                                      .AddTypedClient<AccountAchievementService>()
                                      .AddTypedClient<AchievementService>()
                                      .AddTypedClient<BuildService>()
                                      .AddTypedClient<ColorService>()
                                      .AddTypedClient<ItemService>()
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
