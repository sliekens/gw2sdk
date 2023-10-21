using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.MapChests;

public class MapChestsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "auric_basin_heros_choice_chest",
            "crystal_oasis_heros_choice_chest",
            "domain_of_vabbi_heros_choice_chest"
        };

        var actual = await sut.MapChests.GetMapChestsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
            }
        );
    }
}
