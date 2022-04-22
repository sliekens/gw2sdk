using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts;
using GW2SDK.Achievements;
using GW2SDK.Banking;
using GW2SDK.Colors;
using GW2SDK.Commerce;
using GW2SDK.Crafting;
using GW2SDK.Currencies;
using GW2SDK.Dungeons;
using GW2SDK.Home;
using GW2SDK.Items;
using GW2SDK.ItemStats;
using GW2SDK.MailCarriers;
using GW2SDK.Maps;
using GW2SDK.Masteries;
using GW2SDK.Meta;
using GW2SDK.Mounts;
using GW2SDK.Professions;
using GW2SDK.Quaggans;
using GW2SDK.Skills;
using GW2SDK.Skins;
using GW2SDK.Specializations;
using GW2SDK.Stories;
using GW2SDK.Tokens;
using GW2SDK.Traits;
using GW2SDK.WorldBosses;

namespace GW2SDK.Tests.TestInfrastructure;

public class Composer : IServiceProvider, IAsyncDisposable
{
    private readonly ConfigurationManager configuration = new();

    private readonly CompositeDisposable disposables = new();

    private readonly IHttpClientFactory httpClientFactory;

    public Composer()
    {
        var gw2HttpClientFactory = new TestHttpClientFactory(configuration.BaseAddress);
        disposables.Add(gw2HttpClientFactory);
        httpClientFactory = gw2HttpClientFactory;
    }

    public async ValueTask DisposeAsync()
    {
        await disposables.DisposeAsync().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    public object GetService(Type serviceType)
    {
        if (serviceType == typeof(TestCharacterName))
        {
            return new TestCharacterName(configuration.CharacterName);
        }

        if (serviceType == typeof(ApiKeyBasic))
        {
            return new ApiKeyBasic(configuration.ApiKeyBasic);
        }

        if (serviceType == typeof(ApiKey))
        {
            return new ApiKey(configuration.ApiKey);
        }

        if (serviceType == typeof(HttpClient))
        {
            return httpClientFactory.CreateClient("GW2SDK");
        }

        if (serviceType == typeof(BankQuery))
        {
            return new BankQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(AccountQuery))
        {
            return new AccountQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(AchievementQuery))
        {
            return new AchievementQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(StoryQuery))
        {
            return new StoryQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(DyeQuery))
        {
            return new DyeQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(CommerceQuery))
        {
            return new CommerceQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(MapQuery))
        {
            return new MapQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(WalletQuery))
        {
            return new WalletQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(ItemQuery))
        {
            return new ItemQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(MailCarrierQuery))
        {
            return new MailCarrierQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(CraftingQuery))
        {
            return new CraftingQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(WardrobeQuery))
        {
            return new WardrobeQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(TokenProvider))
        {
            return new TokenProvider(Resolve<HttpClient>());
        }

        if (serviceType == typeof(TraitQuery))
        {
            return new TraitQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(MetaQuery))
        {
            return new MetaQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(ProfessionQuery))
        {
            return new ProfessionQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(ItemStatQuery))
        {
            return new ItemStatQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(SkillQuery))
        {
            return new SkillQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(WorldBossQuery))
        {
            return new WorldBossQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(MasteryQuery))
        {
            return new MasteryQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(SpecializationQuery))
        {
            return new SpecializationQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(MountQuery))
        {
            return new MountQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(QuagganQuery))
        {
            return new QuagganQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(WalletQuery))
        {
            return new WalletQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(HomeQuery))
        {
            return new HomeQuery(Resolve<HttpClient>());
        }

        if (serviceType == typeof(DungeonQuery))
        {
            return new DungeonQuery(Resolve<HttpClient>());
        }

        return null;
    }

    public T Resolve<T>() =>
        (T)GetService(typeof(T))
        ?? throw new InvalidOperationException($"Unable to compose type '{typeof(T)}'");
}
