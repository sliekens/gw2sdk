using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Maps;

[ServiceDataSource]
public class MapsIndex(Gw2Client sut)
{
    [Retry(3, RetryOnExceptionTypes = new[] { typeof(System.Net.Http.HttpRequestException) })]
    [Test]
    [Arguments(1, 0, 1)]
    [Arguments(1, 0, 2)]
    [Arguments(1, 0, 3)]
    public async Task Map_ids_in_a_region_can_be_listed(int continentId, int floorId, int regionId)
    {
        (IImmutableValueSet<int> actual, MessageContext context) = await sut.Exploration.GetMapsIndex(continentId, floorId, regionId, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
    }

    [Retry(3, RetryOnExceptionTypes = new[] { typeof(System.Net.Http.HttpRequestException) })]
    [Test]
    public async Task All_map_ids_can_be_listed()
    {
        (IImmutableValueSet<int> actual, MessageContext context) = await sut.Exploration.GetMapsIndex(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
    }
}
