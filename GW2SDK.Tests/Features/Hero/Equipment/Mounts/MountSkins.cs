using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountSkins
{
    [Fact]
    public async Task Mount_skins_can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.Mounts.GetMountSkins(cancellationToken: TestContext.Current.CancellationToken);

        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry => Assert.True(entry.Mount.IsDefined()));
    }
}
