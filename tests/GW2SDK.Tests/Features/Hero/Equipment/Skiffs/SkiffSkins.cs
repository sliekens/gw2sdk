﻿using System.Text.Json;

using GuildWars2.Hero.Equipment.Skiffs;
using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Equipment.Skiffs;

public class SkiffSkins
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<SkiffSkin> actual, MessageContext context) = await sut.Hero.Equipment.Skiffs.GetSkiffSkins(cancellationToken: TestContext.Current!.CancellationToken);

        Assert.NotEmpty(actual);

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.NotNull(entry.IconUrl);
            Assert.True(entry.IconUrl == null || entry.IconUrl.IsAbsoluteUri || entry.IconUrl.IsWellFormedOriginalString());
            Assert.NotNull(entry.DyeSlots);
            Assert.All(entry.DyeSlots, dyeSlot =>
            {
                Assert.True(dyeSlot.Material.IsDefined());
                Assert.True(dyeSlot.ColorId > 0);
            });
            string json = JsonSerializer.Serialize(entry);
            SkiffSkin? roundtrip = JsonSerializer.Deserialize<SkiffSkin>(json);
            Assert.Equal(entry, roundtrip);
        });
    }
}
