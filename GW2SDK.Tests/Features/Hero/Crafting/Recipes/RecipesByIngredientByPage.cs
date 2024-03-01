using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

public class RecipesByIngredientByPage
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int visionCrystal = 46746;
        const int pageSize = 3;
        var (actual, context) =
            await sut.Hero.Crafting.Recipes.GetRecipesByIngredientItemIdByPage(
                visionCrystal,
                0,
                pageSize
            );

        Assert.Equal(pageSize, actual.Count);
        Assert.Equal(pageSize, actual.Count);
        Assert.NotNull(context.Links);
        Assert.Equal(pageSize, context.PageSize);
        Assert.Equal(context.ResultCount, pageSize);
        Assert.All(
            actual,
            recipe => Assert.Contains(
                recipe.Ingredients,
                ingredient => ingredient.Id == visionCrystal
            )
        );
    }
}
