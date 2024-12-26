using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

public class RecipesIndexByOutputItem
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int ironIngotItemId = 19683;
        var (actual, context) =
            await sut.Hero.Crafting.Recipes.GetRecipesIndexByOutputItemId(ironIngotItemId, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);

        const int ironIngotRecipeId = 19;
        Assert.Contains(ironIngotRecipeId, actual);
    }
}
