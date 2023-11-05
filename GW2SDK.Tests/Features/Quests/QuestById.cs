﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Quests;

public class QuestById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 15;

        var (actual, _) = await sut.Quests.GetQuestById(id);

        Assert.Equal(id, actual.Id);
        actual.Has_name();
        actual.Has_level();
        actual.Has_story();
        actual.Has_goals();
    }
}
