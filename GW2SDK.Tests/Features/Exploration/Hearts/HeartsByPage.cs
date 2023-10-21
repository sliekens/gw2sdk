using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Hearts;

public class HeartsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;

        var actual = await sut.Maps.GetHeartsByPage(continentId, floorId, regionId, mapId, 0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.NotNull(actual.PageContext);
        Assert.Equal(3, actual.PageContext.PageSize);
        actual.Value.All_have_ids();
        actual.Value.Some_have_objectives();
        actual.Value.All_have_chat_links();
    }
}
