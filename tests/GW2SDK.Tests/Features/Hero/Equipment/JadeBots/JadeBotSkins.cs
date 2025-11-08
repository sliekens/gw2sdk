using System.Text.Json;

using GuildWars2.Hero.Equipment.JadeBots;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.JadeBots;

public class JadeBotSkins
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<JadeBotSkin> actual, MessageContext context) = await sut.Hero.Equipment.JadeBots.GetJadeBotSkins(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.Id > 0);
            Assert.NotEmpty(entry.Name);
            // Missing description for Roundtail Dragon
            if (entry.Id == 6)
            {
                Assert.Empty(entry.Description);
            }
            else
            {
                Assert.NotEmpty(entry.Description);
                MarkupSyntaxValidator.Validate(entry.Description);
            }

            Assert.True(entry.UnlockItemId > 0);
            string json = JsonSerializer.Serialize(entry);
            JadeBotSkin? roundtrip = JsonSerializer.Deserialize<JadeBotSkin>(json);
            Assert.Equal(entry, roundtrip);
        });
    }
}
