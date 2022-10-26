using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Guilds.Upgrades;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Guilds.Upgrades;

public class GuildUpgradesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            260,
            630,
            167
        };

        var actual = await sut.Guilds.GetGuildUpgradesByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, actual.Context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_description();
                entry.Has_icon();
                entry.Has_costs();
                if (entry is BankBag bankBag)
                {
                    bankBag.Has_MaxItems();
                    bankBag.Has_MaxCoins();
                }
            }
        );
    }
}
