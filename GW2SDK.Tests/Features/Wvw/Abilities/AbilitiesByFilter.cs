﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Abilities;

public class AbilitiesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            2,
            3,
            4
        };

        var actual = await sut.Wvw.GetAbilitiesByIds(ids);

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
                entry.Has_ranks();
            }
        );
    }
}