using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Guilds.Upgrades;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Guilds.Upgrades;

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

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
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
