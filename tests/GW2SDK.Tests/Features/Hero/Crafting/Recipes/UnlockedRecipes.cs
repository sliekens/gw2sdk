using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Crafting.Recipes;

[ServiceDataSource]
public class UnlockedRecipes(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<int> actual, _) = await sut.Hero.Crafting.Recipes.GetUnlockedRecipes(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.True(id >= 0));
    }
}
