using System.Text.Json;

using GuildWars2.Hero.Equipment.JadeBots;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.JadeBots;

[ServiceDataSource]
public class JadeBotSkins(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<JadeBotSkin> actual, MessageContext context) = await sut.Hero.Equipment.JadeBots.GetJadeBotSkins(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        foreach (JadeBotSkin entry in actual)
        {
            await Assert.That(entry.Id).IsGreaterThan(0);
            await Assert.That(entry.Name).IsNotEmpty();
            // Missing description for Roundtail Dragon
            if (entry.Id == 6)
            {
                await Assert.That(entry.Description).IsEmpty();
            }
            else
            {
                await Assert.That(entry.Description).IsNotEmpty();
                MarkupSyntaxValidator.Validate(entry.Description);
            }

            await Assert.That(entry.UnlockItemId).IsGreaterThan(0);
#if NET
            string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.JadeBotSkin);
            JadeBotSkin? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.JadeBotSkin);
#else
            string json = JsonSerializer.Serialize(entry);
            JadeBotSkin? roundtrip = JsonSerializer.Deserialize<JadeBotSkin>(json);
#endif
            await Assert.That(roundtrip).IsEqualTo(entry);
        }
    }
}
