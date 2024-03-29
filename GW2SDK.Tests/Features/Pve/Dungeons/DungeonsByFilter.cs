﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Dungeons;

public class DungeonsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids =
        [
            "twilight_arbor", "sorrows_embrace",
            "citadel_of_flame"
        ];

        var (actual, context) = await sut.Pve.Dungeons.GetDungeonsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.Contains(entry.Id, ids);
                entry.Has_paths();
                Assert.All(
                    entry.Paths,
                    path =>
                    {
                        path.Has_id();
                        path.Has_kind();
                    }
                );
            }
        );
    }
}
