using GuildWars2.Hero.Crafting.Recipes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

[ServiceDataSource]
public class RecipesByOutputItemByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int ironIngot = 19683;
        const int pageSize = 3;
        (HashSet<Recipe> actual, MessageContext context) = await sut.Hero.Crafting.Recipes.GetRecipesByOutputItemIdByPage(ironIngot, 0, pageSize, cancellationToken: TestContext.Current!.Execution.CancellationToken);
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
