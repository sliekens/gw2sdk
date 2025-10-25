using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Worlds;

namespace GuildWars2.Tests.Features.Worlds;

public class WorldsByFilter
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        HashSet<int> ids = [1001, 1002, 1003];
        (HashSet<World> actual, MessageContext context) = await sut.Worlds.GetWorldsByIds(ids, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.Equal(ids.Count, context.ResultCount);
        Assert.True(context.ResultTotal > ids.Count);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(ids, first => Assert.Contains(actual, found => found.Id == first), second => Assert.Contains(actual, found => found.Id == second), third => Assert.Contains(actual, found => found.Id == third));
    }
}
