using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.MapChests;

public class ReceivedMapChests
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Pve.MapChests.GetReceivedMapChests(accessToken.Key);

        // Can be empty if you haven't done any map metas today
        // The best we can do is verify that there are no unexpected map chests
        Assert.All(
            actual,
            chest => Assert.Contains(
                chest,
                new[]
                {
                    "auric_basin_heros_choice_chest",
                    "crystal_oasis_heros_choice_chest",
                    "domain_of_vabbi_heros_choice_chest",
                    "dragons_stand_heros_choice_chest",
                    "elon_riverlands_heros_choice_chest",
                    "tangled_depths_heros_choice_chest",
                    "the_desolation_heros_choice_chest",
                    "verdant_brink_heros_choice_chest"
                }
            )
        );
    }
}
