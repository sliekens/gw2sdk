﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildLog
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var guildLeader = Composer.Resolve<TestGuildLeader>();

        var (account, _) = await sut.Hero.Account.GetSummary(guildLeader.Token);
        foreach (var guildId in account.LeaderOfGuildIds!)
        {
            var (actual, _) = await sut.Guilds.GetGuildLog(guildId, guildLeader.Token);

            Assert.NotEmpty(actual);

            // While we are here, check the ability to use a log ID as a skip token
            if (actual.Count > 3)
            {
                var skipToken = actual[3].Id;
                var (range, _) = await sut.Guilds.GetGuildLog(
                    guildId,
                    skipToken,
                    guildLeader.Token
                );
                Assert.True(range.Count >= 3);
                Assert.All(range, log => Assert.True(log.Id > skipToken));
            }
        }
    }
}
