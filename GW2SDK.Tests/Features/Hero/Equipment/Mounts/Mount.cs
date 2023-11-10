using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class Mount
{
    [Fact]
    public async Task Can_be_found_by_name()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const MountName name = MountName.Skyscale;

        var (actual, _) = await sut.Hero.Equipment.Mounts.GetMountByName(name);

        Assert.Equal(name, actual.Id);
    }
}
