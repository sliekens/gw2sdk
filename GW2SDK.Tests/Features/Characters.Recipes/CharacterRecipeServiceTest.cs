using System;
using System.Threading.Tasks;
using GW2SDK.Characters.Recipes;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Characters.Recipes
{
    public class CharacterRecipeServiceTest
    {
        [Fact]
        public async Task It_can_get_the_unlocked_recipes()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CharacterRecipesService>();

            var actual = await sut.GetUnlockedRecipes(ConfigurationManager.Instance.CharacterName, ConfigurationManager.Instance.ApiKeyFull);

            Assert.NotEmpty(actual.Recipes);
            Assert.All(actual.Recipes, id => Assert.NotEqual(0, id));
        }

        [Fact]
        public async Task It_cant_get_the_recipes_without_an_access_token()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CharacterRecipesService>();

            var actual = await Record.ExceptionAsync(async () =>
            {
                var _ = await sut.GetUnlockedRecipes("Nobody", null);
            });

            var reason = Assert.IsType<UnauthorizedOperationException>(actual);

            Assert.Equal("Invalid access token", reason.Message);
        }

        [Fact]
        public async Task It_cant_get_the_recipes_for_a_deleted_character()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CharacterRecipesService>();

            var actual = await Record.ExceptionAsync(async () =>
            {
                var _ = await sut.GetUnlockedRecipes("Nobody", ConfigurationManager.Instance.ApiKeyFull);
            });

            var reason = Assert.IsType<ArgumentException>(actual);

            Assert.Equal("no such character", reason.Message);
        }

        [Fact]
        public async Task It_cant_get_the_recipes_without_the_characters_or_inventories_scope()
        {
            await using var services = new Composer();
            var sut = services.Resolve<CharacterRecipesService>();

            var actual = await Record.ExceptionAsync(async () =>
            {
                var _ = await sut.GetUnlockedRecipes("Nobody", ConfigurationManager.Instance.ApiKeyBasic);
            });

            var reason = Assert.IsType<UnauthorizedOperationException>(actual);

            Assert.Equal("requires scope characters", reason.Message);
        }
    }
}
