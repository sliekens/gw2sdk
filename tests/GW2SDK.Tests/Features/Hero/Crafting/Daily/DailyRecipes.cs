using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Crafting.Daily;

[ServiceDataSource]
public class DailyRecipes(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        // This is not resistant to recipes being added to the game, so not great :)
        // For now I'll just maintain this by hand...
        // no clue how this can be solved without re-implementing the call to /v2/dailycrafting in test code (which makes the test pointless)
        (HashSet<string> dailyRecipes, _) = await sut.Hero.Crafting.Daily.GetDailyCraftableItems(TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(new[] { "charged_quartz_crystal", "glob_of_elder_spirit_residue", "lump_of_mithrilium", "spool_of_silk_weaving_thread", "spool_of_thick_elonian_cord" }, dailyRecipes);
        // Again this next method is not deterministic...
        (HashSet<string> actual, _) = await sut.Hero.Crafting.Daily.GetDailyCraftedItems(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        // The best we can do is verify that there are no unexpected recipes
        // i.e. all recipes must be present in the reference data
        Assert.All(actual, recipeId => Assert.Contains(recipeId, dailyRecipes));
    }
}
