using GuildWars2.Guilds.Upgrades;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds.Upgrades;

public class GuildUpgradesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            260,
            630,
            167
        };

        var actual = await sut.Guilds.GetGuildUpgradesByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
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
