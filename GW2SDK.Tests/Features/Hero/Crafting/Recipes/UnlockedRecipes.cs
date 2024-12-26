using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

public class UnlockedRecipes
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Hero.Crafting.Recipes.GetUnlockedRecipes(accessToken.Key, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.True(id >= 0));
    }
}
