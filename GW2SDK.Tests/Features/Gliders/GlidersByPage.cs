﻿using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Gliders;

public class GlidersByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        var actual = await sut.Gliders.GetGlidersByPage(0, pageSize);

        Assert.Equal(pageSize, actual.Value.Count);
        Assert.Equal(pageSize, actual.PageContext.PageSize);
        Assert.Equal(pageSize, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_unlock_items();
                entry.Has_order();
                entry.Has_icon();
                entry.Has_name();
                entry.Has_description();
                entry.Has_default_dyes();
            }
        );
    }
}
