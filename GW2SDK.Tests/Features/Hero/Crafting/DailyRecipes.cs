using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Crafting;

public class DailyRecipes
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        // This is not resistant to recipes being added to the game, so not great :)
        // For now I'll just maintain this by hand...
        // no clue how this can be solved without re-implementing the call to /v2/dailycrafting in test code (which makes the test pointless)
        var (dailyRecipes, _) = await sut.Hero.Crafting.GetDailyRecipes();

        Assert.Equal(
            new[]
            {
                "charged_quartz_crystal",
                "glob_of_elder_spirit_residue",
                "lump_of_mithrilium",
                "spool_of_silk_weaving_thread",
                "spool_of_thick_elonian_cord"
            },
            dailyRecipes
        );

        // Again this next method is not deterministic...
        var (actual, _) = await sut.Hero.Crafting.GetDailyRecipesOnCooldown(accessToken.Key);

        // The best we can do is verify that there are no unexpected recipes
        // i.e. all recipes must be present in the reference data
        Assert.All(actual, recipeId => Assert.Contains(recipeId, dailyRecipes));
    }
}
