using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts;
using GW2SDK.Achievements;
using GW2SDK.Banking;
using GW2SDK.Builds;
using GW2SDK.Colors;
using GW2SDK.Commerce;
using GW2SDK.Crafting;
using GW2SDK.Currencies;
using GW2SDK.Items;
using GW2SDK.ItemStats;
using GW2SDK.MailCarriers;
using GW2SDK.Maps;
using GW2SDK.Masteries;
using GW2SDK.Mounts;
using GW2SDK.Professions;
using GW2SDK.Quaggans;
using GW2SDK.Skills;
using GW2SDK.Skins;
using GW2SDK.Specializations;
using GW2SDK.Stories;
using GW2SDK.Tokens;
using GW2SDK.Traits;
using GW2SDK.V2;
using GW2SDK.WorldBosses;

namespace GW2SDK.Tests.TestInfrastructure
{
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
            await disposables.DisposeAsync()
                .ConfigureAwait(false);
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

            if (serviceType == typeof(ApiKeyFull))
            {
                return new ApiKeyFull(configuration.ApiKeyFull);
            }

            if (serviceType == typeof(HttpClient))
            {
                return httpClientFactory.CreateClient("GW2SDK");
            }

            if (serviceType == typeof(Banker))
            {
                return new Banker(Resolve<HttpClient>());
            }

            if (serviceType == typeof(Account))
            {
                return new Account(Resolve<HttpClient>());
            }

            if (serviceType == typeof(AchievementService))
            {
                return new AchievementService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(StoryJournal))
            {
                return new StoryJournal(Resolve<HttpClient>());
            }

            if (serviceType == typeof(BuildService))
            {
                return new BuildService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(ColorService))
            {
                return new ColorService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(TradingPost))
            {
                return new TradingPost(Resolve<HttpClient>());
            }

            if (serviceType == typeof(WorldMap))
            {
                return new WorldMap(Resolve<HttpClient>());
            }

            if (serviceType == typeof(Wallet))
            {
                return new Wallet(Resolve<HttpClient>());
            }

            if (serviceType == typeof(ItemService))
            {
                return new ItemService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(MailCarrierService))
            {
                return new MailCarrierService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(CraftingStation))
            {
                return new CraftingStation(Resolve<HttpClient>());
            }

            if (serviceType == typeof(Wardrobe))
            {
                return new Wardrobe(Resolve<HttpClient>());
            }

            if (serviceType == typeof(TokenService))
            {
                return new TokenService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(TraitService))
            {
                return new TraitService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(ApiInfoService))
            {
                return new ApiInfoService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(ProfessionService))
            {
                return new ProfessionService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(ItemStatService))
            {
                return new ItemStatService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(SkillService))
            {
                return new SkillService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(WorldBossService))
            {
                return new WorldBossService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(MasteryService))
            {
                return new MasteryService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(SpecializationService))
            {
                return new SpecializationService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(MountService))
            {
                return new MountService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(QuagganService))
            {
                return new QuagganService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(Wallet))
            {
                return new Wallet(Resolve<HttpClient>());
            }

            return null;
        }

        public T Resolve<T>() =>
            (T) GetService(typeof(T)) ?? throw new InvalidOperationException($"Unable to compose type '{typeof(T)}'");
    }
}
