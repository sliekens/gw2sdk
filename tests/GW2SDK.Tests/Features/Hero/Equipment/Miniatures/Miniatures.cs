﻿using System.Text.Json;

using GuildWars2.Hero.Equipment.Miniatures;
using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Equipment.Miniatures;

public class Miniatures
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<Miniature> actual, MessageContext context) = await sut.Hero.Equipment.Miniatures.GetMiniatures(cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotEmpty(actual);

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.True(entry.IconUrl is null || entry.IconUrl.IsAbsoluteUri);
            Assert.True(entry.Order >= 0);
            Assert.True(entry.ItemId >= 0);
            string json = JsonSerializer.Serialize(entry);
            Miniature? roundtrip = JsonSerializer.Deserialize<Miniature>(json);
            Assert.Equal(entry, roundtrip);
        });
    }
}
