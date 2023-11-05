using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Crafting;

public class RecipesByIngredientByPage
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int visionCrystal = 46746;
        const int pageSize = 3;
        var (actual, context) = await sut.Crafting.GetRecipesByIngredientItemIdByPage(visionCrystal, 0, pageSize);

        Assert.Equal(pageSize, actual.Count);
        Assert.Equal(pageSize, actual.Count);
        Assert.NotNull(context.PageContext);
        Assert.Equal(pageSize, context.PageContext.PageSize);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(pageSize, context.ResultContext.ResultCount);
        Assert.All(
            actual,
            recipe => Assert.Contains(
                recipe.Ingredients,
                ingredient => ingredient.Id == visionCrystal
            )
        );
    }
}
