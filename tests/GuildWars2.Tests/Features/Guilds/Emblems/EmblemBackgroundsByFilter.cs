using GuildWars2.Guilds.Emblems;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Emblems;

[ServiceDataSource]
public class EmblemBackgroundsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<int> ids = [1, 2, 3];
        (IImmutableValueSet<EmblemBackground> actual, MessageContext context) = await sut.Guilds.GetEmblemBackgroundsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsNotNull().And.IsGreaterThan(ids.Count));
        await Assert.That(actual).Count().IsEqualTo(ids.Count);
        using (Assert.Multiple())
        {
            foreach (int id in ids)
            {
                await Assert.That(actual).Contains(found => found.Id == id);
            }
        }
    }
}
