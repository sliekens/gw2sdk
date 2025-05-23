﻿using System.Text.Json;
using GuildWars2.Hero.Equipment.Finishers;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Finishers;

public class Finishers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.Finishers.GetFinishers(
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.NotNull(entry.LockedText);
                MarkupSyntaxValidator.Validate(entry.LockedText);
                Assert.NotNull(entry.UnlockItemIds);
                Assert.All(entry.UnlockItemIds, id => Assert.True(id > 0));
                Assert.True(entry.Order >= 0);
                Assert.NotNull(entry.IconUrl);
                Assert.True(entry.IconUrl.IsAbsoluteUri || entry.IconUrl.IsWellFormedOriginalString());
                Assert.NotEmpty(entry.Name);

                var json = JsonSerializer.Serialize(entry);
                var roundTrip = JsonSerializer.Deserialize<Finisher>(json);
                Assert.Equal(entry, roundTrip);
            }
        );
    }
}
