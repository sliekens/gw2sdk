﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Abilities;

public class Abilities
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) =
            await sut.Wvw.GetAbilities(cancellationToken: TestContext.Current.CancellationToken);

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.NotEmpty(entry.Name);
                Assert.NotEmpty(entry.Description);
#pragma warning disable CS0618 // IconHref is obsolete
                Assert.NotEmpty(entry.IconHref);
#pragma warning restore CS0618
                Assert.True(entry.IconUrl.IsAbsoluteUri);
                Assert.NotEmpty(entry.Ranks);
            }
        );
    }
}
