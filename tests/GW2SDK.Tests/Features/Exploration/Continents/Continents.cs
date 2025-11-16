using System.Drawing;

using GuildWars2.Exploration.Continents;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Continents;

[ServiceDataSource]
public class Continents(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Continent> actual, MessageContext context) = await sut.Exploration.GetContinents(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();
        using (Assert.Multiple())
        {
            foreach (Continent entry in actual)
            {
                await Assert.That(entry)
                    .Member(e => e.Id, id => id.IsGreaterThan(0))
                    .And.Member(e => e.Name, name => name.IsNotEmpty())
                    .And.Member(e => e.ContinentDimensions, dim => dim.IsNotEqualTo(Size.Empty))
                    .And.Member(e => e.MinZoom, minZoom => minZoom.IsGreaterThanOrEqualTo(0))
                    .And.Member(e => e.MaxZoom, maxZoom => maxZoom.IsGreaterThan(entry.MinZoom))
                    .And.Member(e => e.Floors, floors => floors.IsNotEmpty());
            }
        }
    }
}
