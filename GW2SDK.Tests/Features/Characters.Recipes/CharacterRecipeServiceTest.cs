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

            var actual = await sut.GetUnlockedRecipes(ConfigurationManager.Instance.CharacterName,
                ConfigurationManager.Instance.ApiKeyFull);

            Assert.NotEmpty(actual.Recipes);
            Assert.All(actual.Recipes, id => Assert.NotEqual(0, id));
        }
    }
}
