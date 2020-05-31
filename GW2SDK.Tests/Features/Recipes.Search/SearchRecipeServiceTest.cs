using System.Threading.Tasks;
using GW2SDK.Recipes.Search;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes.Search
{
    public class SearchRecipeServiceTest
    {
        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Integration")]
        public async Task Recipes_with_iron_ore_as_ingredient_contains_recipe_id_for_iron_ingot()
        {
            await using var services = new Container();
            var sut = services.Resolve<SearchRecipeService>();

            const int ironOre = 19699;
            var actual = await sut.GetRecipesIndexByIngredientId(ironOre);

            const int ironIngotRecipe = 19;
            Assert.Contains(ironIngotRecipe, actual);
        }

        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Integration")]
        public async Task Recipes_with_iron_ingot_as_output_contains_recipe_id_for_iron_ingot()
        {
            await using var services = new Container();
            var sut = services.Resolve<SearchRecipeService>();

            const int ironIngot = 19683;
            var actual = await sut.GetRecipesIndexByItemId(ironIngot);

            const int ironIngotRecipe = 19;
            Assert.Contains(ironIngotRecipe, actual);
        }
    }
}
