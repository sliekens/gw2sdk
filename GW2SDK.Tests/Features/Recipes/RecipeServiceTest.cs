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
        public async Task It_can_get_all_recipe_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<RecipeService>();

            var actual = await sut.GetRecipesIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_a_recipe_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<RecipeService>();

            const int recipeId = 1;

            var actual = await sut.GetRecipeById(recipeId);

            Assert.Equal(recipeId, actual.Value.Id);
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_recipes_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<RecipeService>();

            var ids = new[] { 1, 2, 3 };

            var actual = await sut.GetRecipesByIds(ids);

            Assert.Collection(actual.Values, recipe => Assert.Equal(1, recipe.Id), recipe => Assert.Equal(2, recipe.Id), recipe => Assert.Equal(3, recipe.Id));
        }

        [Fact]
        [Trait("Feature",  "Recipes")]
        [Trait("Category", "Unit")]
        public async Task Recipe_ids_cannot_be_null()
        {
            await using var services = new Composer();
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
            await using var services = new Composer();
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
        public async Task It_can_get_recipes_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<RecipeService>();

            var actual = await sut.GetRecipesByPage(1, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Integration")]
        public async Task Recipes_ids_with_iron_ore_ingredient_contains_recipe_id_for_iron_ingot()
        {
            await using var services = new Composer();
            var sut = services.Resolve<RecipeService>();

            const int ironOre = 19699;
            var actual = await sut.GetRecipesIndexByIngredientItemId(ironOre);

            const int ironIngotRecipe = 19;
            Assert.Contains(ironIngotRecipe, actual.Values);
        }

        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Integration")]
        public async Task Recipes_with_iron_ore_ingredient_contains_recipe_for_iron_ingot()
        {
            await using var services = new Composer();
            var sut = services.Resolve<RecipeService>();

            const int ironOre = 19699;
            var actual = await sut.GetRecipesByIngredientItemId(ironOre);

            const int ironIngotRecipe = 19;
            Assert.Contains(actual.Values, recipe => recipe.Id == ironIngotRecipe);
        }

        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Integration")]
        public async Task Recipes_page_with_iron_ore_ingredient_contains_recipe_for_iron_ingot()
        {
            await using var services = new Composer();
            var sut = services.Resolve<RecipeService>();

            const int ironOre = 19699;
            var actual = await sut.GetRecipesByIngredientItemIdByPage(ironOre, 0, 20);

            const int ironIngotRecipe = 19;
            Assert.Contains(actual.Values, recipe => recipe.Id == ironIngotRecipe);
        }

        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Integration")]
        public async Task Recipes_ids_with_iron_ingot_output_contains_recipe_id_for_iron_ingot()
        {
            await using var services = new Composer();
            var sut = services.Resolve<RecipeService>();

            const int ironIngot = 19683;
            var actual = await sut.GetRecipesIndexByOutputItemId(ironIngot);

            const int ironIngotRecipe = 19;
            Assert.Contains(ironIngotRecipe, actual.Values);
        }

        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Integration")]
        public async Task Recipes_with_iron_ingot_output_contains_recipe_for_iron_ingot()
        {
            await using var services = new Composer();
            var sut = services.Resolve<RecipeService>();

            const int ironIngot = 19683;
            var actual = await sut.GetRecipesByOutputItemId(ironIngot);

            const int ironIngotRecipe = 19;
            Assert.Contains(actual.Values, recipe => recipe.Id == ironIngotRecipe);
        }

        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Integration")]
        public async Task Recipes_page_with_iron_ingot_output_contains_recipe_for_iron_ingot()
        {
            await using var services = new Composer();
            var sut = services.Resolve<RecipeService>();

            const int ironIngot = 19683;
            var actual = await sut.GetRecipesByOutputItemIdByPage(ironIngot, 0, 20);

            const int ironIngotRecipe = 19;
            Assert.Contains(actual.Values, recipe => recipe.Id == ironIngotRecipe);
        }
        
        [Fact]
        [Trait("Feature",  "Recipes.Search")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_recipes_for_vision_crystal()
        {
            // Normally the limit for ids=all is 200 items
            //   but that doesn't seem to apply for recipes search by input/output item
            // There are 800+ recipes that require a vision crystal
            await using var services = new Composer();
            var sut = services.Resolve<RecipeService>();

            const int visionCrystal = 46746;
            var actual = await sut.GetRecipesByIngredientItemId(visionCrystal);

            Assert.NotInRange(actual.Values.Count, 0, 200); // Greater than 200
            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
            Assert.Equal(actual.Context.ResultTotal, actual.Context.ResultCount);
            Assert.All(actual.Values, recipe => Assert.Contains(recipe.Ingredients, ingredient => ingredient.ItemId == visionCrystal));
        }
    }
}
