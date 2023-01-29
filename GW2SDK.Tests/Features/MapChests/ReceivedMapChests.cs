using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.MapChests;

public class ReceivedMapChests
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.MapChests.GetReceivedMapChests(accessToken.Key);

        // Can be empty if you haven't done any map metas today
        // The best we can do is verify that there are no unexpected map chests
        Assert.All(actual.Value, chest => Assert.Contains(chest, new[]
        {
            "auric_basin_heros_choice_chest",
            "crystal_oasis_heros_choice_chest",
            "domain_of_vabbi_heros_choice_chest",
            "dragons_stand_heros_choice_chest",
            "elon_riverlands_heros_choice_chest",
            "tangled_depths_heros_choice_chest",
            "the_desolation_heros_choice_chest",
            "verdant_brink_heros_choice_chest"
        }));
    }
}
