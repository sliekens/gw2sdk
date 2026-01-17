using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Worlds;

[ServiceDataSource]
public class WorldsIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<int> actual, MessageContext context) = await sut.Worlds.GetWorldsIndex(TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, rt => rt.IsEqualTo(actual.Count));
            await Assert.That(actual).IsNotEmpty();
        }
    }
}
