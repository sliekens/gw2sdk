using System.Text.Json;
using GuildWars2.Chat;
using GuildWars2.Hero.Equipment.Outfits;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Outfits;

public class Outfits
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.Outfits.GetOutfits(
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
                Assert.NotEmpty(entry.Name);
                Assert.True(entry.IconUrl is not null && entry.IconUrl.IsAbsoluteUri);
                Assert.NotEmpty(entry.UnlockItemIds);

                var chatLink = entry.GetChatLink();
                Assert.Equal(entry.Id, chatLink.OutfitId);

                var chatLinkRoundtrip = OutfitLink.Parse(chatLink.ToString());
                Assert.Equal(chatLink.ToString(), chatLinkRoundtrip.ToString());

                var json = JsonSerializer.Serialize(entry);
                var roundtrip = JsonSerializer.Deserialize<Outfit>(json);
                Assert.Equal(entry, roundtrip);
            }
        );
    }
}
