using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Mounts;

public class Mounts
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMounts();

        Assert.All(
            actual.Value,
            mount =>
            {
                Assert.True(
                    Enum.IsDefined(typeof(MountName), mount.Id),
                    "Enum.IsDefined(mount.Id)"
                );
                Assert.NotEmpty(mount.Name);
            }
        );
    }
}
