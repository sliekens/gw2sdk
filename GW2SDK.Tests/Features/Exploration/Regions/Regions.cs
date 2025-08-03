using GuildWars2.Exploration.Regions;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Regions;

public class Regions
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    public async Task Can_be_listed(int continentId, int floorId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        (HashSet<Region> actual, MessageContext context) = await sut.Exploration.GetRegions(
            continentId,
            floorId,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, Assert.NotNull);
    }
}
