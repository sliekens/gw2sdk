using System.Threading.Tasks;
using GW2SDK.Guilds.Upgrades;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Guilds.Upgrades;

public class GuildUpgradeById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int guildUpgradeId = 43;

        var actual = await sut.Guilds.GetGuildUpgradeById(guildUpgradeId);

        Assert.Equal(guildUpgradeId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_description();
        actual.Value.Has_icon();
        actual.Value.Has_costs();
        if (actual.Value is BankBag bankBag)
        {
            bankBag.Has_MaxItems();
            bankBag.Has_MaxCoins();
        }
    }
}
