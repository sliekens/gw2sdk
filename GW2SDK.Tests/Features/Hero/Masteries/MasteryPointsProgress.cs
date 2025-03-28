﻿using GuildWars2.Hero.Masteries;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Masteries;

public class MasteryPointsProgress
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Hero.Masteries.GetMasteryPointsProgress(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(actual);
        Assert.Equal(actual.Unlocked.Count, actual.Totals.Sum(total => total.Earned));
        Assert.Contains(
            actual.Totals,
            total => total.Region == MasteryRegionName.Tyria
                && total.Earned > 0
                && total.Spent > 0
                && total.Available <= total.Earned
        );
        Assert.Contains(
            actual.Totals,
            total => total.Region == MasteryRegionName.Maguuma
                && total.Earned > 0
                && total.Spent > 0
                && total.Available <= total.Earned
        );
        Assert.Contains(
            actual.Totals,
            total => total.Region == MasteryRegionName.Desert
                && total.Earned > 0
                && total.Spent > 0
                && total.Available <= total.Earned
        );
        Assert.Contains(
            actual.Totals,
            total => total.Region == MasteryRegionName.Tundra
                && total.Earned > 0
                && total.Spent > 0
                && total.Available <= total.Earned
        );
        Assert.Contains(
            actual.Totals,
            total => total.Region == MasteryRegionName.Jade
                && total.Earned > 0
                && total.Spent > 0
                && total.Available <= total.Earned
        );
        Assert.Contains(
            actual.Totals,
            total => total.Region == MasteryRegionName.Sky
                && total.Earned > 0
                && total.Spent > 0
                && total.Available <= total.Earned
        );
    }
}
