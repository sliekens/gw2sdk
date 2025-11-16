using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Worlds;

namespace GuildWars2.Tests.Features.Worlds;

[ServiceDataSource]
public class WorldsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<int> ids = [1001, 1002, 1003];
        (HashSet<World> actual, MessageContext context) = await sut.Worlds.GetWorldsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).HasCount().EqualTo(ids.Count);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(ids.Count);
            await Assert.That(actual.ElementAt(0).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(1).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(2).Id).IsIn(ids);
        }
    }
}
