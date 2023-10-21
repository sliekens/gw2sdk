using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Sectors;

public class SectorsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        HashSet<int> ids = new()
        {
            513,
            514,
            515
        };

        var actual = await sut.Maps.GetSectorsByIds(continentId, floorId, regionId, mapId, ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
        actual.Value.All_have_ids();
        actual.Value.Some_have_names();
        actual.Value.All_have_chat_links();
    }
}
