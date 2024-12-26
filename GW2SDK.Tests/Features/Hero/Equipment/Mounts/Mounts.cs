using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class Mounts
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, _) = await sut.Hero.Equipment.Mounts.GetMounts(cancellationToken: TestContext.Current.CancellationToken);

        Assert.All(
            actual,
            mount =>
            {
                Assert.True(mount.Id.IsDefined());
                Assert.NotEmpty(mount.Name);
            }
        );
    }
}
