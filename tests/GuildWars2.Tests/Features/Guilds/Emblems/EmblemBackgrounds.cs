using GuildWars2.Guilds.Emblems;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds.Emblems;

[ServiceDataSource]
public class EmblemBackgrounds(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<EmblemBackground> actual, MessageContext context) = await sut.Guilds.GetEmblemBackgrounds(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
        await Assert.That(actual).IsNotEmpty();
        using (Assert.Multiple())
        {
            foreach (EmblemBackground emblem in actual)
            {
                await Assert.That(emblem.Id).IsGreaterThan(0);
                await Assert.That(emblem.LayerUrls).IsNotEmpty();
                foreach (Uri? url in emblem.LayerUrls)
                {
                    await Assert.That(url).IsNotNull();
                }
            }
        }
    }
}
