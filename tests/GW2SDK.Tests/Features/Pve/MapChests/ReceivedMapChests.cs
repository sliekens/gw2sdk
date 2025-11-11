using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Pve.MapChests;

[ServiceDataSource]
public class ReceivedMapChests(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<string> actual, _) = await sut.Pve.MapChests.GetReceivedMapChests(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        // Can be empty if you haven't done any map metas today
        // The best we can do is verify that there are no unexpected map chests
        Assert.All(actual, chest => Assert.Contains(chest, new[] { "amnytas_heros_choice_chest", "auric_basin_heros_choice_chest", "citadel_of_zakiros_heros_choice_chest", "convergence_heros_choice_chest", "crystal_oasis_heros_choice_chest", "domain_of_vabbi_heros_choice_chest", "dragons_end_heros_choice_chest", "dragons_stand_heros_choice_chest", "echovald_wilds_heros_choice_chest", "elon_riverlands_heros_choice_chest", "gyala_delve_heros_choice_chest", "inner_nayos_heros_choice_chest", "new_kaineng_city_heros_choice_chest", "seitung_province_heros_choice_chest", "skywatch_archipelago_heros_choice_chest", "tangled_depths_heros_choice_chest", "the_desolation_heros_choice_chest", "verdant_brink_heros_choice_chest", "wild_island_heros_choice_chest" }));
    }
}
