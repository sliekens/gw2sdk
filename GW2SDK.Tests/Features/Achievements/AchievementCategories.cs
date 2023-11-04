﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Achievements;

public class AchievementCategories
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Achievements.GetAchievementCategories();

        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(actual.Value.Count, actual.Context.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.Context.ResultContext.ResultTotal);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_order();
                entry.Has_icon();
                entry.Has_achievements();

                Assert.All(entry.Achievements,
                    achievement =>
                    {
                        Assert.Empty(achievement.Flags.Other);
                    });
            }
        );
    }
}
