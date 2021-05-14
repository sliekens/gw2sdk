using System;
using System.Net.Http;
using System.Threading.Tasks;
using GW2SDK.Accounts;
using GW2SDK.Accounts.Achievements;
using GW2SDK.Accounts.Banks;
using GW2SDK.Accounts.Recipes;
using GW2SDK.Achievements;
using GW2SDK.Achievements.Categories;
using GW2SDK.Achievements.Dailies;
using GW2SDK.Achievements.Groups;
using GW2SDK.Backstories;
using GW2SDK.Builds;
using GW2SDK.Characters.Recipes;
using GW2SDK.Colors;
using GW2SDK.Commerce.Prices;
using GW2SDK.Continents;
using GW2SDK.Currencies;
using GW2SDK.Items;
using GW2SDK.MailCarriers;
using GW2SDK.Recipes;
using GW2SDK.Skins;
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
        private readonly CompositeDisposable _disposables = new();

        private readonly IHttpClientFactory _httpClientFactory;

        public Composer()
        {
            var gw2HttpClientFactory = new TestHttpClientFactory();
            _disposables.Add(gw2HttpClientFactory);
            _httpClientFactory = gw2HttpClientFactory;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(HttpClient))
            {
                return _httpClientFactory.CreateClient("GW2SDK");
            }

            if (serviceType == typeof(AccountAchievementService))
            {
                return new AccountAchievementService(Resolve<HttpClient>(), new AccountAchievementReader());
            }

            if (serviceType == typeof(BankService))
            {
                return new BankService(Resolve<HttpClient>(), new BankReader());
            }

            if (serviceType == typeof(AccountRecipesService))
            {
                return new AccountRecipesService(Resolve<HttpClient>(), new AccountRecipeReader());
            }

            if (serviceType == typeof(AccountService))
            {
                return new AccountService(Resolve<HttpClient>(), new AccountReader());
            }

            if (serviceType == typeof(AchievementCategoryService))
            {
                return new AchievementCategoryService(Resolve<HttpClient>(), new AchievementCategoryReader());
            }

            if (serviceType == typeof(DailyAchievementService))
            {
                return new DailyAchievementService(Resolve<HttpClient>(), new DailyAchievementReader());
            }

            if (serviceType == typeof(AchievementGroupService))
            {
                return new AchievementGroupService(Resolve<HttpClient>(), new AchievementGroupReader());
            }

            if (serviceType == typeof(AchievementService))
            {
                return new AchievementService(Resolve<HttpClient>(), new AchievementReader());
            }

            if (serviceType == typeof(BackstoryService))
            {
                return new BackstoryService(Resolve<HttpClient>(), new BackstoryReader());
            }

            if (serviceType == typeof(BuildService))
            {
                return new BuildService(Resolve<HttpClient>(), new BuildReader());
            }

            if (serviceType == typeof(CharacterRecipesService))
            {
                return new CharacterRecipesService(Resolve<HttpClient>(), new CharacterReader());
            }

            if (serviceType == typeof(ColorService))
            {
                return new ColorService(Resolve<HttpClient>(), new ColorReader());
            }

            if (serviceType == typeof(ItemPriceService))
            {
                return new ItemPriceService(Resolve<HttpClient>(), new ItemPriceReader());
            }

            if (serviceType == typeof(ContinentService))
            {
                return new ContinentService(Resolve<HttpClient>(), new ContinentReader());
            }

            if (serviceType == typeof(CurrencyService))
            {
                return new CurrencyService(Resolve<HttpClient>(), new CurrencyReader());
            }

            if (serviceType == typeof(ItemService))
            {
                return new ItemService(Resolve<HttpClient>(), new ItemReader());
            }

            if (serviceType == typeof(MailCarrierService))
            {
                return new MailCarrierService(Resolve<HttpClient>(), new MailCarrierReader());
            }

            if (serviceType == typeof(RecipeService))
            {
                return new RecipeService(Resolve<HttpClient>(), new RecipeReader());
            }

            if (serviceType == typeof(SkinService))
            {
                return new SkinService(Resolve<HttpClient>(), new SkinReader());
            }

            if (serviceType == typeof(TokenInfoService))
            {
                return new TokenInfoService(Resolve<HttpClient>(), new TokenInfoReader());
            }

            if (serviceType == typeof(SubtokenService))
            {
                return new SubtokenService(Resolve<HttpClient>(), new SubtokenReader());
            }

            if (serviceType == typeof(TitleService))
            {
                return new TitleService(Resolve<HttpClient>(), new TitleReader());
            }

            if (serviceType == typeof(TraitService))
            {
                return new TraitService(Resolve<HttpClient>(), new TraitReader());
            }

            if (serviceType == typeof(ApiInfoService))
            {
                return new ApiInfoService(Resolve<HttpClient>(), new ApiInfoReader());
            }

            if (serviceType == typeof(WorldService))
            {
                return new WorldService(Resolve<HttpClient>(), new WorldReader());
            }

            return null;
        }

        public T Resolve<T>() =>
            (T) GetService(typeof(T)) ?? throw new InvalidOperationException($"Unable to compose type '{typeof(T)}'");

        public async ValueTask DisposeAsync()
        {
            await _disposables.DisposeAsync().ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }
    }
}
