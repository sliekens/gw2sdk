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
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, resultCount => resultCount.IsEqualTo(actual.Count))
            .And.Member(c => c.ResultTotal, resultTotal => resultTotal.IsEqualTo(actual.Count));
        using (Assert.Multiple())
        {
            foreach (Outfit entry in actual)
            {
                await Assert.That(entry.Id).IsGreaterThan(0);
                await Assert.That(entry.Name).IsNotEmpty();
                await Assert.That(entry.IconUrl is not null && entry.IconUrl.IsAbsoluteUri).IsTrue();
                await Assert.That(entry.UnlockItemIds).IsNotEmpty();
                OutfitLink chatLink = entry.GetChatLink();
                await Assert.That(chatLink.OutfitId).IsEqualTo(entry.Id);
                OutfitLink chatLinkRoundtrip = OutfitLink.Parse(chatLink.ToString());
                await Assert.That(chatLinkRoundtrip.ToString()).IsEqualTo(chatLink.ToString());
#if NET
                string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.Outfit);
                Outfit? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.Outfit);
#else
                string json = JsonSerializer.Serialize(entry);
                Outfit? roundtrip = JsonSerializer.Deserialize<Outfit>(json);
#endif
                await Assert.That(roundtrip).IsEqualTo(entry);
            }
        }
    }
}
