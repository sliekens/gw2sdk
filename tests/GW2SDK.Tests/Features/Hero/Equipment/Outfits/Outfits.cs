using System.Text.Json;

using GuildWars2.Chat;
using GuildWars2.Hero.Equipment.Outfits;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Outfits;

[ServiceDataSource]
public class Outfits(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Outfit> actual, MessageContext context) = await sut.Hero.Equipment.Outfits.GetOutfits(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            Assert.True(entry.IconUrl is not null && entry.IconUrl.IsAbsoluteUri);
            Assert.NotEmpty(entry.UnlockItemIds);
            OutfitLink chatLink = entry.GetChatLink();
            Assert.Equal(entry.Id, chatLink.OutfitId);
            OutfitLink chatLinkRoundtrip = OutfitLink.Parse(chatLink.ToString());
            Assert.Equal(chatLink.ToString(), chatLinkRoundtrip.ToString());
#if NET
            string json = JsonSerializer.Serialize(entry, GuildWars2JsonContext.Default.Outfit);
            Outfit? roundtrip = JsonSerializer.Deserialize(json, GuildWars2JsonContext.Default.Outfit);
#else
            string json = JsonSerializer.Serialize(entry);
            Outfit? roundtrip = JsonSerializer.Deserialize<Outfit>(json);
#endif
            Assert.Equal(entry, roundtrip);
        });
    }
}
