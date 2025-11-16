using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

[ServiceDataSource]
public class MountsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_name()
    {
        HashSet<MountName> names = [MountName.Raptor, MountName.Jackal, MountName.Skimmer];
        (HashSet<Mount> actual, MessageContext context) = await sut.Hero.Equipment.Mounts.GetMountsByNames(names, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(names.Count);
        await Assert.That(context.ResultTotal).IsNotNull().And.IsGreaterThan(names.Count);
        await Assert.That(actual.Count).IsEqualTo(names.Count);
        using (Assert.Multiple())
        {
            foreach (MountName name in names)
            {
                await Assert.That(actual.Any(m => m.Id == name)).IsTrue();
            }
        }
    }
}
