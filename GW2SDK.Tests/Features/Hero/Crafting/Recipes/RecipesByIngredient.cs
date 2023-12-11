using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

public class RecipesByIngredient
{
    [Fact]
    public async Task Can_be_found()
    {
        // Normally the limit for ids=all is 200 items
        //   but that doesn't seem to apply for recipes search by input/output item
        // There are 800+ recipes that require a vision crystal
        var sut = Composer.Resolve<Gw2Client>();

        const int visionCrystal = 46746;
        var (actual, context) =
            await sut.Hero.Crafting.Recipes.GetRecipesByIngredientItemId(visionCrystal);

        Assert.NotInRange(actual.Count, 0, 200); // Greater than 200
        Assert.NotNull(context.ResultContext);
        Assert.Equal(context.ResultContext.ResultTotal, actual.Count);
        Assert.Equal(context.ResultContext.ResultTotal, context.ResultContext.ResultCount);
        Assert.All(
            actual,
            recipe => Assert.Contains(
                recipe.Ingredients,
                ingredient => ingredient.Id == visionCrystal
            )
        );
    }
}
