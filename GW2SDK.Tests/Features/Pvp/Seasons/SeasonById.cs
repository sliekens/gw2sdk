﻿using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

public class SeasonById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const string id = "2B2E80D3-0A74-424F-B0EA-E221500B323C";

        var actual = await sut.Pvp.GetSeasonById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_name();
    }
}