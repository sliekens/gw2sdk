﻿using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Dungeons;

public class DungeonById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const string id = "citadel_of_flame";

        var actual = await sut.Dungeons.GetDungeonById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_paths();
    }
}
