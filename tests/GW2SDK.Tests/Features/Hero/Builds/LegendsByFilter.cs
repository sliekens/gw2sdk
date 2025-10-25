using GuildWars2.Hero.Builds;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Builds;

public class LegendsByFilter
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        HashSet<string> ids = ["Legend1", "Legend3", "Legend5"];
        (HashSet<Legend> actual, MessageContext context) = await sut.Hero.Builds.GetLegendsByIds(ids, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.Equal(ids.Count, context.ResultCount);
        Assert.True(context.ResultTotal > ids.Count);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(ids, first => Assert.Contains(actual, found => found.Id == first), second => Assert.Contains(actual, found => found.Id == second), third => Assert.Contains(actual, found => found.Id == third));
    }
}
