using GuildWars2.Exploration.Sectors;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Sectors;

public class Sectors
{
    [Test]
    [Arguments(1, 0, 1, 26)]
    [Arguments(1, 0, 1, 27)]
    [Arguments(1, 0, 1, 28)]
    public async Task Can_be_listed(int continentId, int floorId, int regionId, int mapId)
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Sector> actual, MessageContext context) = await sut.Exploration.GetSectors(continentId, floorId, regionId, mapId, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, Assert.NotNull);
    }
}
