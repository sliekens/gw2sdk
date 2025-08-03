using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

public class RecipesByOutputItemByPage
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int ironIngot = 19683;
        const int pageSize = 3;
        (HashSet<Recipe> actual, MessageContext context) = await sut.Hero.Crafting.Recipes.GetRecipesByOutputItemIdByPage(
            ironIngot,
            0,
            pageSize,
            cancellationToken: TestContext.Current.CancellationToken
        );

        const int ironIngotRecipe = 19;
        Assert.NotNull(context.Links);
        Assert.Equal(pageSize, context.PageSize);
        Assert.Equal(1, context.ResultCount);
        Assert.Equal(1, context.PageTotal);
        Assert.Equal(1, context.ResultTotal);
        Recipe? found = Assert.Single(actual);
        Assert.Equal(ironIngotRecipe, found.Id);
    }
}
