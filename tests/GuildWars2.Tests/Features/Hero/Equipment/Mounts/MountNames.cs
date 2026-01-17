using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

[ServiceDataSource]
public class MountNames(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Extensible<MountName>> actual, _) = await sut.Hero.Equipment.Mounts.GetMountNames(TestContext.Current!.Execution.CancellationToken);
        foreach (Extensible<MountName> entry in actual)
        {
            await Assert.That(entry.IsDefined()).IsTrue();
        }
    }
}
