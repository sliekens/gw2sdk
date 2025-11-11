using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

[ServiceDataSource]
public class MountByName(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found_by_name()
    {
        const MountName name = MountName.Skyscale;
        (Mount actual, _) = await sut.Hero.Equipment.Mounts.GetMountByName(name, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(name, actual.Id);
    }
}
