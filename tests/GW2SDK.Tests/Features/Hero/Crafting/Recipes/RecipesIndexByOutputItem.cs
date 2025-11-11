using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

[ServiceDataSource]
public class RecipesIndexByOutputItem(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int ironIngotItemId = 19683;
        (HashSet<int> actual, MessageContext context) = await sut.Hero.Crafting.Recipes.GetRecipesIndexByOutputItemId(ironIngotItemId, TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        const int ironIngotRecipeId = 19;
        Assert.Contains(ironIngotRecipeId, actual);
    }
}
