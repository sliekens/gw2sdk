using GuildWars2.Pve.Dungeons;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Dungeons;

[ServiceDataSource]
public class Dungeons(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Dungeon> actual, MessageContext context) = await sut.Pve.Dungeons.GetDungeons(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (Dungeon entry in actual)
        {
            await Assert.That(entry.Id).IsNotEmpty();
            await Assert.That(entry.Paths).IsNotEmpty();
            foreach (DungeonPath path in entry.Paths)
            {
                await Assert.That(path.Id).IsNotEmpty();
                await Assert.That(path.Kind.IsDefined()).IsTrue();
            }
        }
    }
}
