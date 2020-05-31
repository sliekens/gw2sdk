using System;
using System.Threading.Tasks;
using GW2SDK.Recipes;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Recipes
{
    public class RecipeServiceTest
    {
        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public async Task Get_all_recipe_ids()
        {
            await using var services = new Container();
            var sut = services.Resolve<RecipeService>();

            var actual = await sut.GetRecipesIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public async Task Get_a_recipe_by_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<RecipeService>();

            const int recipeId = 1;

            var actual = await sut.GetRecipeById(recipeId);

            Assert.Equal(recipeId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public async Task Get_recipes_by_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<RecipeService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetRecipesByIds(ids);

            Assert.Collection(actual, recipe => Assert.Equal(1, recipe.Id), recipe => Assert.Equal(2, recipe.Id), recipe => Assert.Equal(3, recipe.Id));
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public async Task Recipe_ids_cannot_be_null()
        {
            await using var services = new Container();
            var sut = services.Resolve<RecipeService>();

            await Assert.ThrowsAsync<ArgumentNullException>("recipeIds",
                async () =>
                {
                    await sut.GetRecipesByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public async Task Recipe_ids_cannot_be_empty()
        {
            await using var services = new Container();
            var sut = services.Resolve<RecipeService>();

            await Assert.ThrowsAsync<ArgumentException>("recipeIds",
                async () =>
                {
                    await sut.GetRecipesByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public async Task Get_recipes_by_page()
        {
            await using var services = new Container();
            var sut = services.Resolve<RecipeService>();

            var actual = await sut.GetRecipesByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public async Task Page_index_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<RecipeService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetRecipesByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public async Task Page_size_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<RecipeService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetRecipesByPage(1, -3));
        }
    }
}
