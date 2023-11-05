using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Upgrades;

public class UpgradeById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 57;

        var (actual, _) = await sut.Wvw.GetUpgradeById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_tiers();
    }
}
