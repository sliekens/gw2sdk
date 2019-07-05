using System.Threading.Tasks;
using GW2SDK.Features.Recipes.Search;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes.Search
{
    public class SearchRecipeServiceTest
    {
        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Integration")]
        public async Task GetRecipesIndexByIngredientId_WithIronOreId_ShouldReturnIronIngotRecipeId()
        {
            var services = new Container();
            var sut = services.Resolve<SearchRecipeService>();

            const int ironOre = 19699;
            var actual = await sut.GetRecipesIndexByIngredientId(ironOre);

            const int ironIngotRecipe = 19;
            Assert.Contains(ironIngotRecipe, actual);
        }

        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Integration")]
        public async Task GetRecipesIndexByItemId_WithironIngotId_ShouldReturnIronIngotRecipeId()
        {
            var services = new Container();
            var sut = services.Resolve<SearchRecipeService>();

            const int ironIngot = 19683;
            var actual = await sut.GetRecipesIndexByItemId(ironIngot);

            const int ironIngotRecipe = 19;
            Assert.Contains(ironIngotRecipe, actual);
        }
    }
}
