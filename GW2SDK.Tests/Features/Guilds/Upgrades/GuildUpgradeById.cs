using GuildWars2.Guilds.Upgrades;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Upgrades;

public class GuildUpgradeById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 43;

        var (actual, _) = await sut.Guilds.GetGuildUpgradeById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Has_description();
        actual.Has_icon();
        actual.Has_costs();
        if (actual is BankBag bankBag)
        {
            bankBag.Has_MaxItems();
            bankBag.Has_MaxCoins();
        }
    }
}
