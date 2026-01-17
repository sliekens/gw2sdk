using GuildWars2.Exploration.Regions;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Regions;

[ServiceDataSource]
public class Regions(Gw2Client sut)
{
    [Test]
    [Arguments(1, 0)]
    [Arguments(2, 1)]
    public async Task Can_be_listed(int continentId, int floorId)
    {
        (IImmutableValueSet<Region> actual, MessageContext context) = await sut.Exploration.GetRegions(continentId, floorId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
        using (Assert.Multiple())
        {
            foreach (Region item in actual)
            {
                await Assert.That(item).IsNotNull();
            }
        }
    }
}
