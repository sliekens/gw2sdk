using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class MountsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.Mounts.GetMountsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, context.PageSize);
    }
}
