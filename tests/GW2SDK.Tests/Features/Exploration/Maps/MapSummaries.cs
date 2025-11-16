using System.Drawing;

using GuildWars2.Exploration.Maps;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Maps;

[ServiceDataSource]
public class MapSummaries(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<MapSummary> actual, MessageContext context) = await sut.Exploration.GetMapSummaries(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        using (Assert.Multiple())
        {
            foreach (MapSummary entry in actual)
            {
                await Assert.That(entry.Id).IsGreaterThan(0);
                if (entry.Id == 1150)
                {
                    // Unnamed Salvation Pass (Public) map
                    await Assert.That(entry.Name).IsEmpty();
                }
                else
                {
                    await Assert.That(entry.Name).IsNotEmpty();
                }

                await Assert.That(entry.MinLevel).IsGreaterThanOrEqualTo(0);
                await Assert.That(entry.MaxLevel).IsGreaterThanOrEqualTo(entry.MinLevel);
                await Assert.That(entry.Kind.IsDefined()).IsTrue();
                await Assert.That(entry.Floors).IsNotEmpty();
                await Assert.That(entry.RegionName).IsNotNull();
                await Assert.That(entry.ContinentName).IsNotNull();
                await Assert.That(entry.MapRectangle).IsNotEqualTo(Rectangle.Empty);
                await Assert.That(entry.ContinentRectangle).IsNotEqualTo(Rectangle.Empty);
            }
        }
    }
}
