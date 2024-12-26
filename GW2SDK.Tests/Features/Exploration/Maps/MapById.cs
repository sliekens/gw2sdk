using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class MapById
{
    [Theory]
    [InlineData(1, 0, 1, 26)]
    [InlineData(1, 0, 1, 27)]
    [InlineData(1, 0, 1, 28)]
    public async Task Can_be_found(int continentId, int floorId, int regionId, int mapId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Exploration.GetMapById(continentId, floorId, regionId, mapId, cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotNull(context);
        Assert.Equal(mapId, actual.Id);
    }
}
