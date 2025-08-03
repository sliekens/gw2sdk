using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountByName
{
    [Fact]
    public async Task Can_be_found_by_name()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const MountName name = MountName.Skyscale;

        (Mount actual, _) = await sut.Hero.Equipment.Mounts.GetMountByName(
            name,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.Equal(name, actual.Id);
    }
}
