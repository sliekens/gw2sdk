using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts;
using GW2SDK.Accounts.Achievements;
using GW2SDK.Accounts.Banks;
using GW2SDK.Achievements;
using GW2SDK.Achievements.Categories;
using GW2SDK.Achievements.Dailies;
using GW2SDK.Achievements.Groups;
using GW2SDK.Backstories;
using GW2SDK.Builds;
using GW2SDK.Colors;
using GW2SDK.Commerce.Prices;
using GW2SDK.Continents;
using GW2SDK.Exceptions;
using GW2SDK.Http;
using GW2SDK.Items;
using GW2SDK.Recipes;
using GW2SDK.Recipes.Search;
using GW2SDK.Skins;
using GW2SDK.Subtokens;
using GW2SDK.Tokens;
using GW2SDK.Worlds;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Timeout;

namespace GW2SDK.Tests.TestInfrastructure
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
                        http.BaseAddress = ConfigurationManager.Instance.BaseAddress;
                        http.UseLatestSchemaVersion();
                        http.UseDataCompression();
                    })
                .ConfigurePrimaryHttpMessageHandler(() =>
                    new SocketsHttpHandler
                    {
                        MaxConnectionsPerServer = 10,
                        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
                    })
                .AddPolicyHandlerFromRegistry((registry, message) => message.Method == HttpMethod.Post || message.Method == HttpMethod.Patch
                    ? registry.Get<IAsyncPolicy<HttpResponseMessage>>("Http")
                    : registry.Get<IAsyncPolicy<HttpResponseMessage>>("HttpIdempotent"))
                .AddHttpMessageHandler<UnauthorizedMessageHandler>()
                .AddHttpMessageHandler<BadMessageHandler>()
                .AddHttpMessageHandler<RateLimitHandler>()
                .AddTypedClient<AccountService>()
                .AddTypedClient<AccountAchievementService>()
                .AddTypedClient<AchievementService>()
                .AddTypedClient<AchievementCategoryService>()
                .AddTypedClient<AchievementGroupService>()
                .AddTypedClient<BackstoryService>()
                .AddTypedClient<BankService>()
                .AddTypedClient<BuildService>()
                .AddTypedClient<ColorService>()
                .AddTypedClient<ContinentService>()
                .AddTypedClient<DailyAchievementService>()
                .AddTypedClient<ItemService>()
                .AddTypedClient<ItemPriceService>()
                .AddTypedClient<RecipeService>()
                .AddTypedClient<SearchRecipeService>()
                .AddTypedClient<SkinService>()
                .AddTypedClient<SubtokenService>()
                .AddTypedClient<TokenInfoService>()
                .AddTypedClient<WorldService>();

            _services = services.BuildServiceProvider();
        }

        public ValueTask DisposeAsync() => _services.DisposeAsync();

        public void Dispose() => _services.Dispose();

        public T Resolve<T>() => _services.GetRequiredService<T>();
    }
}
