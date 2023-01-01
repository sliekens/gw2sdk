﻿using System.Threading.Tasks;
using GuildWars2.Guilds.Upgrades;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Guilds.Upgrades;

public class GuildUpgradesByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        var actual = await sut.Guilds.GetGuildUpgradesByPage(0, pageSize);

        Assert.Equal(pageSize, actual.Value.Count);
        Assert.Equal(pageSize, actual.PageContext.PageSize);
        Assert.Equal(pageSize, actual.ResultContext.ResultCount);
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
