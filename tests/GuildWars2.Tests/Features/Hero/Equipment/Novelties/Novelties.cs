using System.Text.Json;

using GuildWars2.Hero.Equipment.Novelties;
using GuildWars2.Tests.Features.Markup;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Equipment.Novelties;

[ServiceDataSource]
public class Novelties(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Novelty> actual, MessageContext context) = await sut.Hero.Equipment.Novelties.GetNovelties(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);

        using (Assert.Multiple())
        {
            foreach (Novelty entry in actual)
            {
                await Assert.That(entry.Id).IsGreaterThan(0);
                await Assert.That(entry.Name).IsNotEmpty();
                await Assert.That(entry.Description).IsNotNull();
                MarkupSyntaxValidator.Validate(entry.Description);
                await Assert.That(entry.IconUrl.IsAbsoluteUri).IsTrue();
                await Assert.That(entry.Slot.IsDefined()).IsTrue();
                await Assert.That(entry.UnlockItemIds).IsNotEmpty();
#if NET
                string json = JsonSerializer.Serialize(entry, Common.TestJsonContext.Default.Novelty);
                Novelty? roundtrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.Novelty);
#else
                string json = JsonSerializer.Serialize(entry);
                Novelty? roundtrip = JsonSerializer.Deserialize<Novelty>(json);
#endif
                await Assert.That(entry).IsEqualTo(roundtrip);
            }
        }
    }
}
