using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Crafting;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Crafting;

public class CraftingStationTest
{
    [Fact]
    public async Task It_can_get_all_recipe_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();

        var actual = await sut.GetRecipesIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task It_can_get_a_recipe_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();

        const int recipeId = 1;

        var actual = await sut.GetRecipeById(recipeId);

        Assert.Equal(recipeId, actual.Value.Id);
    }

    [Fact]
    public async Task It_can_get_recipes_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.GetRecipesByIds(ids)
            .ToListAsync();

        Assert.Collection(actual,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids));
    }

    [Fact]
    public async Task It_can_get_recipes_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();

        var actual = await sut.GetRecipesByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task Recipes_ids_with_iron_ore_ingredient_contains_recipe_id_for_iron_ingot()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();

        const int ironOre = 19699;
        var actual = await sut.GetRecipesIndexByIngredientItemId(ironOre);

        const int ironIngotRecipe = 19;
        Assert.Contains(ironIngotRecipe, actual);
    }

    [Fact]
    public async Task Recipes_with_iron_ore_ingredient_contains_recipe_for_iron_ingot()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();

        const int ironOre = 19699;
        var actual = await sut.GetRecipesByIngredientItemId(ironOre);

        const int ironIngotRecipe = 19;
        Assert.Contains(actual, recipe => recipe.Id == ironIngotRecipe);
    }

    [Fact]
    public async Task Recipes_page_with_iron_ore_ingredient_contains_recipe_for_iron_ingot()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();

        const int ironOre = 19699;
        var actual = await sut.GetRecipesByIngredientItemIdByPage(ironOre, 0, 20);

        const int ironIngotRecipe = 19;
        Assert.Contains(actual.Values, recipe => recipe.Id == ironIngotRecipe);
    }

    [Fact]
    public async Task Recipes_ids_with_iron_ingot_output_contains_recipe_id_for_iron_ingot()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();

        const int ironIngot = 19683;
        var actual = await sut.GetRecipesIndexByOutputItemId(ironIngot);

        const int ironIngotRecipe = 19;
        Assert.Contains(ironIngotRecipe, actual);
    }

    [Fact]
    public async Task Recipes_with_iron_ingot_output_contains_recipe_for_iron_ingot()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();

        const int ironIngot = 19683;
        var actual = await sut.GetRecipesByOutputItemId(ironIngot);

        const int ironIngotRecipe = 19;
        Assert.Contains(actual, recipe => recipe.Id == ironIngotRecipe);
    }

    [Fact]
    public async Task Recipes_page_with_iron_ingot_output_contains_recipe_for_iron_ingot()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();

        const int ironIngot = 19683;
        var actual = await sut.GetRecipesByOutputItemIdByPage(ironIngot, 0, 20);

        const int ironIngotRecipe = 19;
        Assert.Contains(actual.Values, recipe => recipe.Id == ironIngotRecipe);
    }

    [Fact]
    public async Task It_can_get_all_recipes_for_vision_crystal()
    {
        // Normally the limit for ids=all is 200 items
        //   but that doesn't seem to apply for recipes search by input/output item
        // There are 800+ recipes that require a vision crystal
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();

        const int visionCrystal = 46746;
        var actual = await sut.GetRecipesByIngredientItemId(visionCrystal);

        Assert.NotInRange(actual.Count, 0, 200); // Greater than 200
        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.Equal(actual.Context.ResultTotal, actual.Context.ResultCount);
        Assert.All(actual, recipe => Assert.Contains(recipe.Ingredients, ingredient => ingredient.Id == visionCrystal));
    }

    [Fact(Skip =
        "This test is best used interactively, otherwise it will hit rate limits in this as well as other tests.")]
    public async Task It_can_get_all_recipes()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();

        await foreach (var actual in sut.GetRecipes())
        {
            RecipeFacts.Validate(actual);
        }
    }

    [Fact]
    public async Task It_can_get_the_unlocked_recipes()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();
        var accessToken = services.Resolve<ApiKeyFull>();

        var actual = await sut.GetUnlockedRecipes(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(actual.Value, id => Assert.NotEqual(0, id));
    }

    [Fact]
    public async Task It_can_get_the_learned_recipes()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();
        var characterName = services.Resolve<TestCharacterName>();
        var accessToken = services.Resolve<ApiKeyFull>();

        var actual = await sut.GetLearnedRecipes(characterName.Name, accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(actual.Value, id => Assert.NotEqual(0, id));
    }

    [Fact]
    public async Task It_can_get_the_daily_recipes()
    {
        await using Composer services = new();
        var sut = services.Resolve<CraftingStation>();
        var accessToken = services.Resolve<ApiKeyFull>();

        // This is not resistant to recipes being added to the game, so not great :)
        // For now I'll just maintain this by hand...
        // no clue how this can be solved without re-implementing the call to /v2/dailycrafting in test code (which makes the test pointless)
        var referenceData = await sut.GetDailyRecipes();
        var dailyRecipes = referenceData.Value;

        Assert.Equal(new[]
            {
                "charged_quartz_crystal",
                "glob_of_elder_spirit_residue",
                "lump_of_mithrilium",
                "spool_of_silk_weaving_thread",
                "spool_of_thick_elonian_cord"
            },
            dailyRecipes);

        // Again this next method is not deterministic...
        var actual = await sut.GetDailyRecipesOnCooldown(accessToken.Key);

        // The best we can do is verify that there are no unexpected recipes
        // i.e. all recipes must be present in the reference data
        Assert.All(actual.Value, recipeId => dailyRecipes.Contains(recipeId));
    }
}
