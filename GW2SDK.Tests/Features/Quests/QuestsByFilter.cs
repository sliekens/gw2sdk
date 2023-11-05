﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Quests;

public class QuestsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            15,
            16,
            17
        };

        var (actual, context) = await sut.Quests.GetQuestsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(ids.Count, context.ResultContext.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_level();
                entry.Has_story();
                entry.Has_goals();
            }
        );
    }
}
