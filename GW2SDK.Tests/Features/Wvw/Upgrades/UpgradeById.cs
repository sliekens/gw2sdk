using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Upgrades;

public class UpgradeById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int upgradeId = 57;

        var actual = await sut.Wvw.GetUpgradeById(upgradeId);

        Assert.Equal(upgradeId, actual.Value.Id);
        actual.Value.Has_tiers();
    }
}
