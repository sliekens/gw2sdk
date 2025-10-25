using GuildWars2.Exploration.Floors;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Floors;

public class FloorsByFilter
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        const int continentId = 1;
        HashSet<int> ids = [0, 1, 2];
        (HashSet<Floor> actual, MessageContext context) = await sut.Exploration.GetFloorsByIds(continentId, ids, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.Equal(ids.Count, context.ResultCount);
        Assert.True(context.ResultTotal > ids.Count);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(ids, first => Assert.Contains(actual, found => found.Id == first), second => Assert.Contains(actual, found => found.Id == second), third => Assert.Contains(actual, found => found.Id == third));
    }
}
