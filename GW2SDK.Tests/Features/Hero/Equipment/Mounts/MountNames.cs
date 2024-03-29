using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountNames
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Hero.Equipment.Mounts.GetMountNames();

        Assert.All(actual,
            entry =>
            {
                Assert.True(entry.IsDefined());
            });
    }
}
