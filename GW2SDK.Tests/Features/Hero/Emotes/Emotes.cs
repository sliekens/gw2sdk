﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Emotes;

public class Emotes
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Hero.Emotes.GetEmotes(
                cancellationToken: TestContext.Current.CancellationToken
            );

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.NotEmpty(entry.Id);
                Assert.NotEmpty(entry.Commands);
                Assert.NotEmpty(entry.UnlockItemIds);
            }
        );
    }
}
