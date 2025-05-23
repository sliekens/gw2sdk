﻿using GuildWars2.Chat;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Objectives;

public class Objectives
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Wvw.GetObjectives(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.Id);
                Assert.NotEmpty(entry.Name);
                Assert.True(entry.SectorId > 0);
                Assert.True(entry.MapId > 0);
                Assert.True(entry.MapKind.IsDefined());
                Assert.True(entry.MarkerIconUrl is null or { IsAbsoluteUri: true });

                var chatLink = entry.GetChatLink();
                Assert.NotEmpty(entry.ChatLink);
                Assert.Equal(entry.ChatLink, chatLink.ToString());
                Assert.Equal(entry.MapId, chatLink.MapId);
                Assert.Equal(entry.Id, $"{chatLink.MapId}-{chatLink.ObjectiveId}");

                var chatLinkRoundtrip = ObjectiveLink.Parse(chatLink.ToString());
                Assert.Equal(chatLink.ToString(), chatLinkRoundtrip.ToString());
            }
        );
    }
}
