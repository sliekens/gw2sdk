﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AccountAchievements
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, context) = await sut.Hero.Achievements.GetAccountAchievements(accessToken.Key);

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            achievement =>
            {
                Assert.True(achievement.Id > 0);
                Assert.True(achievement.Current >= 0);
                Assert.True(achievement.Max >= 0);
                Assert.True(achievement.Repeated >= 0);
            }
        );
    }
}
