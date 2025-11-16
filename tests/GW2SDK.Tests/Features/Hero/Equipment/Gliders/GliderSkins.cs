using System.Text.Json;

using GuildWars2.Hero.Equipment.Gliders;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Gliders;

[ServiceDataSource]
public class GliderSkins(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<GliderSkin> actual, MessageContext context) = await sut.Hero.Equipment.Gliders.GetGliderSkins(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);

        using (Assert.Multiple())
        {
            foreach (GliderSkin entry in actual)
            {
                await Assert.That(entry.Id).IsGreaterThan(0);
                await Assert.That(entry.UnlockItemIds).IsNotNull();
                await Assert.That(entry.Order).IsGreaterThanOrEqualTo(0);
                await Assert.That(entry.IconUrl.IsAbsoluteUri).IsTrue();
                await Assert.That(entry.Name).IsNotEmpty();
                await Assert.That(entry.Description).IsNotNull();
                MarkupSyntaxValidator.Validate(entry.Description);
                await Assert.That(entry.DefaultDyeColorIds).IsNotNull();
#if NET
                string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.GliderSkin);
                GliderSkin? roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.GliderSkin);
#else
                string json = JsonSerializer.Serialize(entry);
                GliderSkin? roundTrip = JsonSerializer.Deserialize<GliderSkin>(json);
#endif
                await Assert.That(entry).IsEqualTo(roundTrip);
            }
        }
    }
}
