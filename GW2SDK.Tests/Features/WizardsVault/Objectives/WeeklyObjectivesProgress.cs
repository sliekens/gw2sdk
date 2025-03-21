﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.WizardsVault.Objectives;

public class WeeklyObjectivesProgress
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, context) = await sut.WizardsVault.GetWeeklyObjectivesProgress(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.True(actual.RewardItemId > 0);
        Assert.True(actual.RewardAcclaim > 0);
        Assert.True(actual.Progress >= 0);
        Assert.True(actual.Goal >= 0);
        Assert.Equal(actual.Progress, actual.Objectives.Count(objective => objective.Claimed));
        Assert.NotEmpty(actual.Objectives);
        Assert.All(
            actual.Objectives,
            objective =>
            {
                Assert.True(objective.Id > 0);
                Assert.NotEmpty(objective.Title);
                Assert.True(objective.Track.IsDefined());
                Assert.True(objective.RewardAcclaim > 0);
            }
        );
    }
}
