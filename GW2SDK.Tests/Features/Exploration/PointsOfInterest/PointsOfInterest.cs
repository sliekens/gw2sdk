using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.PointsOfInterest;

public class PointsOfInterest
{
    [Theory]
    [InlineData(1, 0, 1, 26)]
    [InlineData(1, 0, 1, 27)]
    [InlineData(1, 0, 1, 28)]
    public async Task Can_be_listed(int continentId, int floorId, int regionId, int mapId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Exploration.GetPointsOfInterest(continentId, floorId, regionId, mapId);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, Assert.NotNull);
    }
}
