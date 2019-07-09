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
        public async Task GetRecipesIndex_ShouldReturnAllIds()
        {
            var services = new Container();
            var sut = services.Resolve<RecipeService>();

            var actual = await sut.GetRecipesIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public async Task GetRecipeById_ShouldReturnThatRecipe()
        {
            var services = new Container();
            var sut = services.Resolve<RecipeService>();

            const int recipeId = 1;

            var actual = await sut.GetRecipeById(recipeId);

            Assert.Equal(recipeId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public async Task GetRecipesByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var services = new Container();
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
        public async Task GetRecipesByIds_WithIdsEmpty_ShouldThrowArgumentException()
        {
            var services = new Container();
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
        public async Task GetRecipesByIds_ShouldReturnThoseRecipes()
        {
            var services = new Container();
            var sut = services.Resolve<RecipeService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetRecipesByIds(ids);

            Assert.Collection(actual, recipe => Assert.Equal(1, recipe.Id), recipe => Assert.Equal(2, recipe.Id), recipe => Assert.Equal(3, recipe.Id));
        }
        
        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public async Task GetRecipesByPage_WithInvalidPage_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<RecipeService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetRecipesByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public async Task GetRecipesByPage_WithInvalidPageSize_ShouldThrowArgumentException()
        {
            var services = new Container();
            var sut = services.Resolve<RecipeService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetRecipesByPage(1, -3));
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public async Task GetRecipesByPage_WithPage1AndPageSize3_ShouldReturnThatPage()
        {
            var services = new Container();
            var sut = services.Resolve<RecipeService>();

            var actual = await sut.GetRecipesByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }
    }
}
