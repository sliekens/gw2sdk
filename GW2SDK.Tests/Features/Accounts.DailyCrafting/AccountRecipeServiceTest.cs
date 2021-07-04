using System.Threading.Tasks;
using GW2SDK.Accounts.DailyCrafting;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.DailyCrafting
{
    public class DailyCraftingServiceTest
    {
        [Fact]
        public async Task It_can_get_the_daily_recipes()
        {
            await using var services = new Composer();
            var sut = services.Resolve<DailyCraftingService>();

            // This is not resistant to recipes being added to the game, so not great :)
            // For now I'll just maintain this by hand...
            // no clue how this can be solved without re-implementing the call to /v2/dailycrafting in test code (which makes the test pointless)
            var referenceData = await sut.GetDailyRecipes();
            var dailyRecipes = referenceData.Value;
            
            Assert.Equal(new[]
            {
                "charged_quartz_crystal",
                "glob_of_elder_spirit_residue",
                "lump_of_mithrilium",
                "spool_of_silk_weaving_thread",
                "spool_of_thick_elonian_cord"
            }, dailyRecipes);

            // Again this next method is not deterministic...
            var actual = await sut.GetDailyRecipesOnCooldown(ConfigurationManager.Instance.ApiKeyFull);

            // The best we can do is verify that there are no unexpected recipes
            // i.e. all recipes must be present in the reference data
            Assert.All(actual.Value, recipeId => dailyRecipes.Contains(recipeId));
        }
    }
}
