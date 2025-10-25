﻿using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Accounts;

public class Account
{
    [Test]
    public async Task Basic_summary_with_any_access_token()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKeyBasic accessToken = TestConfiguration.ApiKeyBasic;
        (AccountSummary actual, _) = await sut.Hero.Account.GetSummary(accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual.DisplayName);
        Assert.NotEmpty(actual.Access);
        Assert.All(actual.Access, product =>
        {
            Assert.True(product.IsDefined());
            Assert.NotEqual(ProductName.None, product);
        });
        Assert.NotEqual(TimeSpan.Zero, actual.Age);
        Assert.Null(actual.LeaderOfGuildIds);
        Assert.Null(actual.FractalLevel);
        Assert.Null(actual.DailyAchievementPoints);
        Assert.Null(actual.MonthlyAchievementPoints);
        Assert.NotNull(actual.Wvw);
        Assert.Null(actual.Wvw.Rank);
    }

    [Test]
    public async Task Full_summary_with_high_trust_access_token()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;
        (AccountSummary actual, _) = await sut.Hero.Account.GetSummary(accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual.DisplayName);
        Assert.NotEmpty(actual.Access);
        Assert.All(actual.Access, product =>
        {
            Assert.True(product.IsDefined());
            Assert.NotEqual(ProductName.None, product);
        });
        Assert.NotNull(actual.LeaderOfGuildIds);
        Assert.NotEqual(TimeSpan.Zero, actual.Age);
        Assert.NotEqual(default, actual.Created);
        Assert.NotNull(actual.FractalLevel);
        Assert.NotNull(actual.DailyAchievementPoints);
        Assert.NotNull(actual.MonthlyAchievementPoints);
        Assert.NotNull(actual.Wvw);
        Assert.NotNull(actual.Wvw.Rank);
    }
}
