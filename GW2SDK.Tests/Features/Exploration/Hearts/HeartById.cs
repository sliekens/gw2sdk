using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Hearts;

public class HeartById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        const int heartId = 2;

        var (actual, _) = await sut.Maps.GetHeartById(continentId, floorId, regionId, mapId, heartId);

        Assert.Equal(heartId, actual.Id);
    }
}
