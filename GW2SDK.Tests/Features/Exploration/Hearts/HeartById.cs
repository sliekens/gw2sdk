using GuildWars2.Exploration.Hearts;
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

        (Heart actual, MessageContext context) = await sut.Exploration.GetHeartById(
            continentId,
            floorId,
            regionId,
            mapId,
            heartId,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(context);
        Assert.Equal(heartId, actual.Id);
    }
}
