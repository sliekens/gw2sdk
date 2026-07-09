using GuildWars2.Exploration.Floors;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Floors;

[ServiceDataSource]
public class FloorsByFilter(Gw2Client sut)
{
    [Retry(3, RetryOnExceptionTypes = new[] { typeof(System.Net.Http.HttpRequestException) })]
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        const int continentId = 1;
        HashSet<int> ids = [0, 1, 2];
        (IImmutableValueSet<Floor> actual, MessageContext context) = await sut.Exploration.GetFloorsByIds(continentId, ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
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
