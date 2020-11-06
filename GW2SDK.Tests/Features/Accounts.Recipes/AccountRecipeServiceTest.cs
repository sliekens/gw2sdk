using System.Threading.Tasks;
using GW2SDK.Accounts.Recipes;
using GW2SDK.Exceptions;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Recipes
{
    public class AccountRecipeServiceTest
    {
        [Fact]
        public async Task It_can_get_the_unlocked_recipes()
        {
            await using var services = new Container();
            var sut = services.Resolve<AccountRecipesService>();

            var actual = await sut.GetUnlockedRecipes(ConfigurationManager.Instance.ApiKeyFull);

            Assert.NotEmpty(actual);
            Assert.All(actual, id => Assert.NotEqual(0, id));
        }

        [Fact]
        public async Task It_cant_get_the_recipes_without_an_access_token()
        {
            await using var services = new Container();
            var sut = services.Resolve<AccountRecipesService>();

            var actual = await Record.ExceptionAsync(async () =>
            {
                var _ = await sut.GetUnlockedRecipes(null);
            });

            var reason = Assert.IsType<UnauthorizedOperationException>(actual);

            Assert.Equal("Invalid access token", reason.Message);
        }

        [Fact]
        public async Task It_cant_get_the_recipes_without_the_unlock_scope()
        {
            await using var services = new Container();
            var sut = services.Resolve<AccountRecipesService>();

            var actual = await Record.ExceptionAsync(async () =>
            {
                var _ = await sut.GetUnlockedRecipes(ConfigurationManager.Instance.ApiKeyBasic);
            });

            var reason = Assert.IsType<UnauthorizedOperationException>(actual);

            Assert.Equal("requires scope unlocks", reason.Message);
        }
    }
}
