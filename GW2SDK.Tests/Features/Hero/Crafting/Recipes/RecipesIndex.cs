using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

public class RecipesIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<int> actual, MessageContext context) =
            await sut.Hero.Crafting.Recipes.GetRecipesIndex(TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
