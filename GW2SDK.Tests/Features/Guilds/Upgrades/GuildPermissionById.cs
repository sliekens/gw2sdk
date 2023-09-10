using System.Threading.Tasks;
using GuildWars2.Guilds.Upgrades;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Guilds.Upgrades;

public class GuildUpgradeById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 43;

        var actual = await sut.Guilds.GetGuildUpgradeById(id);

        Assert.Equal(id, actual.Value.Id);
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
