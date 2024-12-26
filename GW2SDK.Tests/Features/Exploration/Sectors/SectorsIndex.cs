using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Sectors;

public class SectorsIndex
{
    [Theory]
    [InlineData(1, 0, 1, 26)]
    [InlineData(1, 0, 1, 27)]
    [InlineData(1, 0, 1, 28)]
    public async Task Can_be_listed(int continentId, int floorId, int regionId, int mapId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Exploration.GetSectorsIndex(continentId, floorId, regionId, mapId, cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
