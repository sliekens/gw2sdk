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
        var actual =
            await sut.Crafting.GetRecipesByIngredientItemIdByPage(visionCrystal, 0, pageSize);

        Assert.Equal(pageSize, actual.Value.Count);
        Assert.Equal(pageSize, actual.Value.Count);
        Assert.NotNull(actual.Context.PageContext);
        Assert.Equal(pageSize, actual.Context.PageContext.PageSize);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(pageSize, actual.Context.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            recipe => Assert.Contains(
                recipe.Ingredients,
                ingredient => ingredient.Id == visionCrystal
            )
        );
    }
}
