using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts;
using GW2SDK.Accounts.Achievements;
using GW2SDK.Accounts.Banks;
using GW2SDK.Accounts.DailyCrafting;
using GW2SDK.Accounts.Recipes;
using GW2SDK.Accounts.Wallet;
using GW2SDK.Accounts.WorldBosses;
using GW2SDK.Achievements;
using GW2SDK.Achievements.Categories;
using GW2SDK.Achievements.Dailies;
using GW2SDK.Achievements.Groups;
using GW2SDK.Backstories;
using GW2SDK.Builds;
using GW2SDK.Characters;
using GW2SDK.Colors;
using GW2SDK.Commerce;
using GW2SDK.Continents;
using GW2SDK.Currencies;
using GW2SDK.Items;
using GW2SDK.ItemStats;
using GW2SDK.MailCarriers;
using GW2SDK.Masteries;
using GW2SDK.Mounts;
using GW2SDK.Professions;
using GW2SDK.Quaggans;
using GW2SDK.Recipes;
using GW2SDK.Skills;
using GW2SDK.Skins;
using GW2SDK.Specializations;
using GW2SDK.Subtokens;
using GW2SDK.Titles;
using GW2SDK.Tokens;
using GW2SDK.Traits;
using GW2SDK.V2;
using GW2SDK.Worlds;

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

            if (serviceType == typeof(AccountAchievementService))
            {
                return new AccountAchievementService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(BankService))
            {
                return new BankService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(AccountRecipesService))
            {
                return new AccountRecipesService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(AccountService))
            {
                return new AccountService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(AchievementCategoryService))
            {
                return new AchievementCategoryService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(DailyAchievementService))
            {
                return new DailyAchievementService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(AchievementGroupService))
            {
                return new AchievementGroupService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(AchievementService))
            {
                return new AchievementService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(BackstoryService))
            {
                return new BackstoryService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(BuildService))
            {
                return new BuildService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(CharacterService))
            {
                return new CharacterService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(ColorService))
            {
                return new ColorService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(TradingPost))
            {
                return new TradingPost(Resolve<HttpClient>());
            }

            if (serviceType == typeof(ContinentService))
            {
                return new ContinentService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(CurrencyService))
            {
                return new CurrencyService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(ItemService))
            {
                return new ItemService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(MailCarrierService))
            {
                return new MailCarrierService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(RecipeService))
            {
                return new RecipeService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(SkinService))
            {
                return new SkinService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(TokenInfoService))
            {
                return new TokenInfoService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(SubtokenService))
            {
                return new SubtokenService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(TitleService))
            {
                return new TitleService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(TraitService))
            {
                return new TraitService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(ApiInfoService))
            {
                return new ApiInfoService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(WorldService))
            {
                return new WorldService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(ProfessionService))
            {
                return new ProfessionService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(ItemStatService))
            {
                return new ItemStatService(Resolve<HttpClient>());
            }

            if (serviceType == typeof(DailyCraftingService))
            {
                return new DailyCraftingService(Resolve<HttpClient>());
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

            if (serviceType == typeof(WalletService))
            {
                return new WalletService(Resolve<HttpClient>());
            }

            return null;
        }

        public T Resolve<T>() =>
            (T) GetService(typeof(T)) ?? throw new InvalidOperationException($"Unable to compose type '{typeof(T)}'");
    }
}
