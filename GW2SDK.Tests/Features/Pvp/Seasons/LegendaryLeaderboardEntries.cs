﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

public class LegendaryLeaderboardEntries
{
    [Theory]
    [InlineData("2B2E80D3-0A74-424F-B0EA-E221500B323C", "legendary", "eu")]
    [InlineData("2B2E80D3-0A74-424F-B0EA-E221500B323C", "legendary", "na")]
    public async Task Can_be_found(string seasonId, string boardId, string regionId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Pvp.GetLeaderboardEntries(
            seasonId,
            boardId,
            regionId,
            0,
            200,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotNull(context.Links);

        Assert.All(
            actual,
            entry =>
            {
                Assert.Empty(entry.GuildId);
                Assert.Empty(entry.TeamName);
                Assert.Null(entry.TeamId);
            }
        );
    }
}
