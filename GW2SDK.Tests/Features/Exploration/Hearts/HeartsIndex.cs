using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Hearts;

public class HeartsIndex
{
    [Theory]
    [InlineData(1, 0, 1, 26)]
    [InlineData(1, 0, 1, 27)]
    [InlineData(1, 0, 1, 28)]
    public async Task Can_be_listed(int continentId, int floorId, int regionId, int mapId)
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<int> actual, MessageContext context) = await sut.Exploration.GetHeartsIndex(
            continentId,
            floorId,
            regionId,
            mapId,
            TestContext.Current.CancellationToken
        );

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
