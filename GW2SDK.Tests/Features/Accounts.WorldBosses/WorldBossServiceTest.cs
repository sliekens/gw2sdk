using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Accounts.WorldBosses;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.WorldBosses
{
    public class WorldBossServiceTest
    {
        [Fact]
        public async Task It_can_get_the_world_bosses()
        {
            await using var services = new Composer();
            var sut = services.Resolve<WorldBossService>();
            var accessToken = services.Resolve<ApiKeyFull>();

            // This is not resistant to bosses being added to the game, so not great :)
            // For now I'll just maintain this by hand...
            // no clue how this can be solved without re-implementing the call to /v2/worldbosses in test code (which makes the test pointless)
            var referenceData = await sut.GetWorldBosses();
            var worldBosses = referenceData.Value;
            
            Assert.Equal(new[]
            {
                "admiral_taidha_covington",
                "claw_of_jormag",
                "drakkar",
                "fire_elemental",
                "great_jungle_wurm",
                "inquest_golem_mark_ii",
                "karka_queen",
                "megadestroyer",
                "modniir_ulgoth",
                "shadow_behemoth",
                "svanir_shaman_chief",
                "tequatl_the_sunless",
                "the_shatterer",
                "triple_trouble_wurm"
            }, worldBosses);

            // Again this next method is not deterministic...
            var actual = await sut.GetWorldBossesOnCooldown(accessToken.Key);

            // The best we can do is verify that there are no unexpected bosses
            // i.e. all bosses must be present in the reference data
            Assert.All(actual.Value, worldBossId => worldBosses.Contains(worldBossId));
        }
    }
}
